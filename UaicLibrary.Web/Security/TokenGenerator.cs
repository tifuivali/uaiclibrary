using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Web.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IUserProvider userProvider;
        private readonly JwtIssuerOptions jwtOptions;
        private readonly ILogger logger;

        public TokenGenerator(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory,IUserProvider userProvider)
        {
            this.userProvider = userProvider;
            logger = loggerFactory.CreateLogger<TokenGenerator>();
            this.jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(this.jwtOptions);
        }

        public async Task<string> GenerateTokenForUser(User user)
        {
            var identity = await GetClaimsIdentity(user);
            if (identity == null)
            {
                logger.LogInformation(
                    $"Invalid email ({user.Email}) or password ({user.Password})");
                return null;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64),
                identity.FindFirst("Group")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                jwtOptions.NotBefore,
                jwtOptions.Expiration,
                jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long) Math.Round((date.ToUniversalTime() -
                                  new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        /// <summary>
        ///     IMAGINE BIG RED WARNING SIGNS HERE!
        ///     You'd want to retrieve claims through your claims provider
        ///     in whatever way suits you, the below is purely for demo purposes!
        /// </summary>
        private Task<ClaimsIdentity> GetClaimsIdentity(User user)
        {
            var userOut = userProvider.GetUserByEmailAndPassword(user.Email, user.Password);
            if (userOut == null)
                return Task.FromResult<ClaimsIdentity>(null);

            return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(user.Email, "Token"),
                new[]
                {
                    new Claim("Group", userOut.Role)
                }));
        }
    }
}
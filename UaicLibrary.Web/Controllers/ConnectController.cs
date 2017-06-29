using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UaicLibrary.Common.Error;
using UaicLibrary.Domain.UserManagement;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]")]
    public class ConnectController : BaseApiController
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly JwtIssuerOptions jwtOptions;

        public ConnectController(ITokenGenerator tokenGenerator, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.tokenGenerator = tokenGenerator;
            this.jwtOptions = jwtOptions.Value;
            serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
 
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] User applicationUser)
        {

            var encodedJwt = await tokenGenerator.GenerateTokenForUser(applicationUser);
            if (encodedJwt == null)
            {
                var result = Result.Fail("InvalidUserCreditals");
                return ValidationResult(result);
            }
            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int) jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, serializerSettings);
            return new OkObjectResult(json);
        }

    }
}
using System;
using System.Net.WebSockets;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.Domain.UserManagement;
using UaicLibrary.Web.AutofacModules;
using UaicLibrary.Web.Controllers;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web
{
    public class Startup
    {
        private readonly SymmetricSecurityKey signingKey;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsEnvironment("Development"))
                builder.AddApplicationInsightsSettings(true);
            AutoMapperConfig();
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            var secretKey = Configuration["Security:SecretKey"];
            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }

        public IConfigurationRoot Configuration { get; }

        private void AutoMapperConfig()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(UserRepository), typeof(UserController)));
        }

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", delegate (CorsPolicyBuilder policyBuilder)
                {
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.WithOrigins("http://localhost:4200");
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });
            });

            // Add framework services.
            var connsctionString = Configuration["ConnectionStrings:ConnectionString"];
            services.AddDbContext<UaicLibraryContext>(opt => opt.UseNpgsql(connsctionString));
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy(AuthPolicy.Student, policy => policy.RequireClaim("Group", "Student"));
                    options.AddPolicy(AuthPolicy.Professor, policy => policy.RequireClaim("Group", "Professor"));
                });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    config.Filters.Add(new AuthorizeFilter(policy));

                })
                .AddJsonOptions(
                    jsonOptions => { jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });
            services.AddOptions();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DataBaseModule>();
            builder.RegisterModule<RepositoriesModule>();
            builder.RegisterModule<WebModule>();
            builder.RegisterModule<ValidatorsModule>();
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            // Shows UseCors with named policy.
            app.UseCors("AllowSpecificOrigin");

            app.UseStaticFiles();

            app.UseApplicationInsightsExceptionTelemetry();


            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });


            app.UseMvc();
            //app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}
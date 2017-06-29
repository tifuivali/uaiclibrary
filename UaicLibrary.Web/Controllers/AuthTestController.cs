using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthTestController : Controller
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public AuthTestController()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpGet]
        [Authorize(Policy = AuthPolicy.Student)]
        public IActionResult Get()
        {
            var response = new
            {
                made_it = $"Welcome {User}!"
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
    }

}

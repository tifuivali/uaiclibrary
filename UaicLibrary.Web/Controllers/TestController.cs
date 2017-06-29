using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CorsPolicy = UaicLibrary.Web.Security.CorsPolicy;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {
            return "Everything works ok....";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

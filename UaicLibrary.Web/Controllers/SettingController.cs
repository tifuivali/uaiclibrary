using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.SettingsManagement;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]/[action]")]
    public class SettingController: BaseApiController
    {
        private readonly ISettingRepository settingRepository;

        public SettingController(ISettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        [HttpGet]
        public ActionResult GetHomePageContent()
        {
            var result = settingRepository.GetHomePageContent();
            return ValidationResult(result);
        }

        [HttpPost]
        public ActionResult UpdateHomePageContent([FromBody] string content)
        {
            var result = settingRepository.UpdateHomePageContent(content);
            return ValidationJsonResult(result);
        }
    }
}

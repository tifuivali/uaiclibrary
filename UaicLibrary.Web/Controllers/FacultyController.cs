using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.FacultyManagement;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]/[action]")]
    public class FacultyController : BaseApiController
    {
        private readonly IFacultyRepository facultyRepository;
        public FacultyController(IFacultyRepository facultyRepository)
        {
            this.facultyRepository = facultyRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            var faculties = facultyRepository.GetAllFaculties();
            return Ok(faculties);
        }

        [HttpGet]
        public ActionResult GetFaculty(int facultyId)
        {
            var result = facultyRepository.GetFacultyById(facultyId);
            return ValidationResult(result);
        }

        [HttpPost]
        public ActionResult SaveFaculty([FromBody] Faculty faculty)
        {
            var result = facultyRepository.SaveFaculty(faculty);
            return ValidationResult(result);
        }

        [HttpPost]
        public ActionResult RemoveFaculty(int facultyId)
        {
            var result = facultyRepository.RemoveFacultyById(facultyId);
            return ValidationResult(result);
        }
    }
}

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.StudentManagement;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]/[action]")]
    public class StudentController : BaseApiController
    {
        private readonly IStudentRepository studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = studentRepository.GetStudentByEmail(CurrentUserEmail);
            if (result.IsFailure)
                return ErrorResult(result);
            return Ok(result.Value);
        }

        [HttpPut]
        public ActionResult UpdateDescription([FromBody] string description)
        {
            studentRepository.UpdateDescription(CurrentUserId, description);
            return Ok();
        }

    }
}

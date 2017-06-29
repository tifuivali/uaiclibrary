using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.Domain;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Web.Controllers
{
    public class BaseApiController : Controller
    {
        private readonly IGeneralRepository generalRepository;
        public BaseApiController()
        {
            generalRepository = new GeneralRepository(new UaicLibraryContext());
        }

        public int CurrentUserId => generalRepository.GetStudentId(CurrentUserEmail);
        private IList<Claim> Claims => HttpContext.User.Claims.ToList();
        public string CurrentUserEmail => Claims[0].Value;
        public string CurrentUserRole => Claims[3].Value;
        public User CurrentUser => generalRepository.GetUserByEmail(CurrentUserEmail);

        public ActionResult ValidationResult(Result result)
        {
            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        public ActionResult ValidationResult<T>(Result<T> result)
        {
            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        public ActionResult ValidationJsonResult<T>(Result<T> result)
        {
            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }

            return Json(result.Value);
        }

        public ActionResult ErrorResult(Result result)
        {
            return BadRequest(result.Errors);
        }

        public string CurrentHostName => "http://" + Request.HttpContext.Request.Host.Value;
    }
}

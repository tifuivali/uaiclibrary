using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Common.Error;
using UaicLibrary.Domain.StudentManagement;
using UaicLibrary.Domain.UserManagement;
using UaicLibrary.Web.ModelValidators;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseApiController
    {
        private readonly IHostingEnvironment appEnvironment;
        private readonly IUserRepository userRepository;
        private readonly IUserProvider userProvider;
        private readonly IStudentRepository studentRepository;

        public UserController(
            IHostingEnvironment appEnvironment,
            IUserRepository userRepository, 
            IUserProvider userProvider,
            IStudentRepository studentRepository)
        {
            this.appEnvironment = appEnvironment;
            this.userRepository = userRepository;
            this.userProvider = userProvider;
            this.studentRepository = studentRepository;
        }

        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            if (user.ImageUrl == null)
            {
                var hostname = "http://" + Request.HttpContext.Request.Host.Value;
                var url = $"{hostname}/images/avatars/default.jpg";
                user.ImageUrl = url;
            }
            var result = userRepository.CreateUser(user);
            return result.IsSuccess ? Ok(result.Value) : ValidationResult(result);
        }

        public async Task<ActionResult> UploadAvatar(IFormFile file)
        {
            var imageValidator = new ImageValidator();
            var validationResult = imageValidator.Validate(file);
            if (validationResult.IsFailure)
            {
                return ValidationResult(validationResult);
            }
            try
            {
                var savePath = Path.Combine(appEnvironment.WebRootPath, "images","avatars", file.FileName);

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                
                var url = $"{CurrentHostName}/images/avatars/{file.FileName}";
                if (CurrentUserRole == UserRole.Student)
                {
                    studentRepository.UpdateAvatarUrl(CurrentUserId, url);
                }
                else
                {
                    //todo Update image url for prof 
                }
                return Ok(url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public Result<User> GetCurrentUser()
        {
            return Result.Ok(CurrentUser);
        }

        [HttpGet]
        public ActionResult GetLatestOpennedBooks(int count)
        {
            var latestOpennedBooks = userRepository.GetLatestOpennedBooks(count, CurrentUser.Id);
            return Ok(latestOpennedBooks);
        }

        [HttpGet]
        public ActionResult GetLatestAnnotedBooks(int count)
        {
            var latestAnnotedBooks = userRepository.GetLatestAnnotedBooks(count, CurrentUser.Id);
            return Ok(latestAnnotedBooks);
        }
    }
}

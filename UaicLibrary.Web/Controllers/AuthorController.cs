using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.AuthorManagement;
using UaicLibrary.Web.ModelValidators;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{

    namespace UaicLibrary.Web.Controllers
    {
        [EnableCors(CorsPolicy.AllowSpecificOrigin)]
        [Route("api/[controller]/[action]")]
        public class AuthorController : BaseApiController
        {
            private readonly IHostingEnvironment appEnvironment;
            private readonly IAuthorRepository authorRepository;

            public AuthorController(IHostingEnvironment appEnvironment, IAuthorRepository authorRepository)
            {
                this.appEnvironment = appEnvironment;
                this.authorRepository = authorRepository;
            }

            [HttpGet]
            public ActionResult GetAuthors()
            {
                var authors = authorRepository.GetAllAuthors();
                return Ok(authors);
            }

            [HttpGet]
            public ActionResult GetAuthor(int authorId)
            {
                var result = authorRepository.GetAuthorById(authorId);
                return ValidationResult(result);
            }

            [HttpPost]
            public async Task<ActionResult> UploadAuthorAvatar(IFormFile file)
            {
                var imageValidator = new ImageValidator();
                var validationResult = imageValidator.Validate(file);
                if (validationResult.IsFailure)
                {
                    return ValidationResult(validationResult);
                }
                try
                {
                    var savePath = Path.Combine(appEnvironment.WebRootPath, "images", "authors", file.FileName);

                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var url = $"{CurrentHostName}/images/authors/{file.FileName}";

                    return Ok(url);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [HttpPost]
            public ActionResult SaveAuthor([FromBody] Author author)
            {
                var result = authorRepository.SaveAuthor(author);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult RemoveAuthor([FromBody] int authorId)
            {
                var result = authorRepository.RemoveAuthorById(authorId);
                return ValidationResult(result);
            }

        }
    }

}

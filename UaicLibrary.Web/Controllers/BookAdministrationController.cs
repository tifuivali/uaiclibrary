using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.BookManagement;
using UaicLibrary.Web.ModelValidators;
using UaicLibrary.Web.Security;


namespace UaicLibrary.Web.Controllers
{
    [EnableCors(CorsPolicy.AllowSpecificOrigin)]
    [Route("api/[controller]/[action]")]
    public class BookAdministrationController : BaseApiController
    {
        private readonly IHostingEnvironment appEnvironment;
        private readonly IBookRepository bookRepository;
        public BookAdministrationController(IHostingEnvironment appEnvironment, IBookRepository bookRepository)
        {
            this.appEnvironment = appEnvironment;
            this.bookRepository = bookRepository;
        }

        [HttpPost]
        public ActionResult SaveBook([FromBody] BookAdministrationModel bookAdministrationModel)
        {
            var result = bookRepository.SaveBook(bookAdministrationModel);
            return ValidationResult(result);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            var imageValidator = new ImageValidator();
            var validationResult = imageValidator.Validate(file);
            if (validationResult.IsFailure)
            {
                return ValidationResult(validationResult);
            }
            try
            {
                var savePath = Path.Combine(appEnvironment.WebRootPath, "images", "books", file.FileName);

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var url = $"{CurrentHostName}/images/books/{file.FileName}";
                
                return Ok(url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> UploadContent(IFormFile file)
        {
            var imageValidator = new ImageValidator();
            var validationResult = imageValidator.ValidatePdfFile(file);
            if (validationResult.IsFailure)
            {
                return ValidationResult(validationResult);
            }
            try
            {
                var savePath = Path.Combine(appEnvironment.WebRootPath, "books",file.FileName);

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var url = $"{CurrentHostName}/books/{file.FileName}";

                return Ok(url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult RemoveBook([FromBody] int bookId )
        {
            var result = bookRepository.RemoveBookById(bookId);
            return ValidationResult(result);
        }

    }
}

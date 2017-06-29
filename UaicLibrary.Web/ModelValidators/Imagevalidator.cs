using Microsoft.AspNetCore.Http;
using UaicLibrary.Common.Error;
using UaicLibrary.Web.Constatnts;

namespace UaicLibrary.Web.ModelValidators
{
    public class ImageValidator : IModelValidator<IFormFile>
    {
        public Result Validate(IFormFile model)
        {
            return Result.Combine(ValidateFileLength(model), ValidateType(model));
        }

        public Result ValidatePdfFile(IFormFile model)
        {
            return Result.Combine(ValidateFileLength(model), ValidatePdfType(model));
        }

        private Result ValidateFileLength(IFormFile model)
        {
            return model.Length <= 0 ? Result.Fail("NullFileError") : Result.Ok();
        }

        private Result ValidateType(IFormFile model)
        {
            return IsValidContentType(model.ContentType) ? Result.Ok() : Result.Fail("ContentTypeIsNotAnAcceptedImage");
        }

        private Result ValidatePdfType(IFormFile model)
        {
            return model.ContentType == ContentTypes.Pdf ? Result.Ok() : Result.Fail("ContentTypeIsNotPdf");
        }

        private bool IsValidContentType(string contentType)
        {
            if (contentType == ContentTypes.GifImage
                || contentType == ContentTypes.PngImage
                || contentType == ContentTypes.JpegImage
                || contentType == ContentTypes.JpgImage)
            {
                return true;
            }

            return false;
        }
    }
}

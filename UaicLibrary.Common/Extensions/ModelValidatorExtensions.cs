using UaicLibrary.Common.Error;

namespace UaicLibrary.Common.Extensions
{
    public static class ModelValidatorExtensions
    {
        public static Result ValidateStringMaxLength<T>(
            this IModelValidator<T> modelValidator,
            string field,
            string fieldName,
            int maxLength)
        {
            return field.Length > maxLength ? Result.Fail(Error.Error.New("StringLengthMaxError", fieldName)) : Result.Ok();
        }


        public static Result ValidateStringMinMaxLength<T>(
            this IModelValidator<T> modelValidator,
            string field,
            string fieldName,
            int minLength,
            int maxLength)
        {
            return field.Length < minLength && field.Length > maxLength 
                ? Result.Fail(Error.Error.New("StringLengthMinMaxError", fieldName, minLength, maxLength)) 
                : Result.Ok();
        }

        public static Result ValidateStringNullOrWhiteSpace<T>(
           this IModelValidator<T> modelValidator,
           string field,
           string fieldName,
           int minLength,
           int maxLength)
        {
            return string.IsNullOrWhiteSpace(field)
                ? Result.Fail(Error.Error.New("FieldIsRequired", fieldName, minLength, maxLength))
                : Result.Ok();
        }
    }
}

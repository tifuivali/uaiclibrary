namespace UaicLibrary.Common.Error
{
    public interface IModelValidator<in T>
    {
        Result Validate(T model);
    }
}

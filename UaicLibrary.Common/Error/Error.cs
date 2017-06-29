namespace UaicLibrary.Common.Error
{
    public class Error
    {
        public string ErrorMessage { get; set; }
        public object[] Parameters { get; set; }

        private Error(string errorMessage, params object[] parameters)
        {
            ErrorMessage = errorMessage;
            Parameters = parameters;
        }

        public static Error New(string errorMesage, params object[] parameters)
        {
            return new Error(errorMesage, parameters);
        }
    }
}

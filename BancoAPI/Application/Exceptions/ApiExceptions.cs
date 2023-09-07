using System.Globalization;


namespace Application.Exceptions
{
    public class ApiExceptions :Exception
    {
        public ApiExceptions() : base ("")
        {
            
        }
        public ApiExceptions(string message) : base(message) 
        {
                
        }
        public ApiExceptions(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) 
        {
                
        }
    }
}

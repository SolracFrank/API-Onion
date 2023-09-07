using Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next) {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeded = false, Message = error?.Message };


                switch (error)
                {
                    case Application.Exceptions.ApiExceptions e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Application.Exceptions.ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errores = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        //unhandle error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError ;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}

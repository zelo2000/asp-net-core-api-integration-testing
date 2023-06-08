using OZ.UserApi.Services.Exceptions;
using System.Net;

namespace OZ.UserApi.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError; // Default status code for unhandled exceptions

            // Customize the response based on the exception type
            if (exception is ApplicationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is EntityNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(exception.Message);
        }
    }
}

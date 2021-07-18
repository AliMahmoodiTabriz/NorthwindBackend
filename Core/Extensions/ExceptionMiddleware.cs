using Core.Utility.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

              await  HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            string result = new ErrorDetails
            {
                Message = message,
                StatusCode = httpContext.Response.StatusCode,
            }.ToString();


            GetValidationException(httpContext, e,ref message,ref result);

            return httpContext.Response.WriteAsync(result);
        }

        private void GetValidationException(HttpContext httpContext, Exception e, ref string message, ref string result)
        {
            if(e.GetType()==typeof(ValidationException))
            {
                var exeption = (ValidationException)e;
                httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                result = new ErrorDetails
                {
                    Message= exeption.Message,
                    StatusCode = httpContext.Response.StatusCode,
                }.ToString();
            }
        }
    }
}

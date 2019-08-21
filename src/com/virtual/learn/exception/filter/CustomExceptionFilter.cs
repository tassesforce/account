using System.Net;
using compte.Exceptions;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace compte.Exception.Filter
{
    ///<summary>Filter qui va renvoyer le bon message d'erreur</summary>
    internal class CustomExceptionFilter : IExceptionFilter
    {

        private readonly ILogger logger;
 
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            this.logger = logger;
        }
        
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = string.Empty;
    
            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(KnownException) || exceptionType == typeof(InvalidRequestException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnknownException))
            {
                status = HttpStatusCode.NotFound;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
            }
            message = context.Exception.Message;
            context.ExceptionHandled=true;
    
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message + " " + context.Exception.StackTrace;
            logger.LogInformation("Erreur lors de l'éxécution : " + status + " " + err);
            response.WriteAsync(err);
        }
    }
}
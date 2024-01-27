using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Exceptions
{
    /// <summary>
    /// Global level exception handler.
    /// </summary>
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> _logger;
        public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Exception occurred: {exception.Message}");

            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case ForbiddenAccessException fae:
                    problemDetails.Status = StatusCodes.Status403Forbidden;
                    problemDetails.Title = "Forbidden Access Error";
                    problemDetails.Detail = fae.Message;
                    break;
                case NotFoundException nfe:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Not Found Exception";
                    problemDetails.Detail = nfe.Message;
                    break;
                case UnauthorizedAccessException uae:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Title = "Unauthorized Access Error";
                    problemDetails.Detail = uae.Message;
                    break;
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.Message;
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}

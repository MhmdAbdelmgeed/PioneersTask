using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace PioneersTask.Fillters
{
    public class LogActivityFilter : IAsyncActionFilter, IExceptionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"-----------------Start Executing  {context.ActionDescriptor.DisplayName} On Controller {context.Controller} With Parameters {JsonSerializer.Serialize(context.ActionArguments)}----------------------");

            await next();

            _logger.LogInformation($"--------------------End Executing  {context.ActionDescriptor.DisplayName} Finished Execution On Controller {context.Controller}--------------------------");


        }


        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"An error occurred: {context.Exception.Message}");

            // custom error response
            context.Result = new ObjectResult("An error occurred")
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}

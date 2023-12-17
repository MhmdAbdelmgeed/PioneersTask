using System.Diagnostics;

namespace PioneersTask.MiddleWares
{
    public class ProfilingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleWare> _logger;

        public ProfilingMiddleWare(RequestDelegate next, ILogger<ProfilingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        //Determine Request Timing To Completed
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request `{context.Request.Path}` Took `{stopwatch.ElapsedMilliseconds}ms` to Execute");
        }
    }
}

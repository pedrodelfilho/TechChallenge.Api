using Prometheus;

namespace TechChallenge.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class LatencyMiddlewareExtension
    {
        private readonly RequestDelegate _next;
        private readonly Histogram _latencyHistogram;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public LatencyMiddlewareExtension(RequestDelegate next)
        {
            _next = next;

            _latencyHistogram = Metrics.CreateHistogram("techchallenge_api_ms", "Latência do endpoint",
                new HistogramConfiguration
                {
                    Buckets = Histogram.ExponentialBuckets(start: 2, factor: 2, count: 10),
                    LabelNames = ["controller", "action", "status_code"]
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            try
            {
                await _next(context);
            }
            finally
            {
                var endTime = DateTime.UtcNow;
                var latencyMilliseconds = (endTime - startTime).TotalMilliseconds;

                var controller = context.GetRouteValue("controller")?.ToString();
                var action = context.GetRouteValue("action")?.ToString();

                var statusCode = context.Response.StatusCode;

                _latencyHistogram.WithLabels(controller, action, statusCode.ToString()).Observe(latencyMilliseconds);
            }
        }
    }
}

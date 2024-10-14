using Prometheus;

namespace TechChallenge.Api.Extensions
{
    /// <summary>
    /// Middleware para medir a latência de requisições.
    /// </summary>
    public class LatencyMiddlewareExtension
    {
        private readonly RequestDelegate _next;
        private readonly Histogram _latencyHistogram;
        private static readonly Counter _requestCounter = Metrics.CreateCounter("techchallenge_api_requests_total", "Total de requisições HTTP.");
        private static readonly Counter _errorCounter = Metrics.CreateCounter("techchallenge_api_requests_errors_total", "Total de requisições que resultaram em erro.");
        private static readonly Counter _httpMethodCounter = Metrics.CreateCounter("techchallenge_api_requests_by_method_total", "Total de requisições agrupadas por método HTTP.", new[] { "method" });

        /// <summary>
        /// Construtor para o middleware.
        /// </summary>
        /// <param name="next">Delegado para o próximo middleware.</param>
        public LatencyMiddlewareExtension(RequestDelegate next)
        {
            _next = next;

            _latencyHistogram = Metrics.CreateHistogram("techchallenge_api_latency_ms", "Latência do endpoint em milissegundos",
                new HistogramConfiguration
                {
                    Buckets = Histogram.ExponentialBuckets(start: 2, factor: 2, count: 10),
                    LabelNames = new[] { "controller", "action", "status_code" }
                });
        }

        /// <summary>
        /// Método para processar a requisição.
        /// </summary>
        /// <param name="context">Contexto da requisição HTTP.</param>
        /// <returns>Uma tarefa assíncrona.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            try
            {
                // Contar a requisição com base no método HTTP
                _httpMethodCounter.WithLabels(context.Request.Method).Inc();

                await _next(context);
                _requestCounter.Inc();
            }
            catch
            {
                _errorCounter.Inc();
                throw;
            }
            finally
            {
                var endTime = DateTime.UtcNow;
                var latencyMilliseconds = (endTime - startTime).TotalMilliseconds;

                var controller = context.GetRouteValue("controller")?.ToString() ?? "unknown";
                var action = context.GetRouteValue("action")?.ToString() ?? "unknown";
                var statusCode = context.Response.StatusCode;

                // Registrar a latência
                _latencyHistogram.WithLabels(controller, action, statusCode.ToString()).Observe(latencyMilliseconds);
            }
        }
    }
}

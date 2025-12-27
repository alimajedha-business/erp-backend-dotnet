using Prometheus;
using System.Diagnostics;

namespace NGErp.API.Middleware
{
    public class PythonApiMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PythonApiMetricsMiddleware> _logger;

        // Prometheus metrics
        private static readonly Counter PythonApiRequestsTotal = Metrics.CreateCounter(
            "python_api_requests_total",
            "Total number of requests to Python API",
            new CounterConfiguration
            {
                LabelNames = new[] { "endpoint", "method", "status_code" }
            });

        private static readonly Histogram PythonApiRequestDuration = Metrics.CreateHistogram(
            "python_api_request_duration_seconds",
            "Duration of Python API requests in seconds",
            new HistogramConfiguration
            {
                LabelNames = new[] { "endpoint", "method" },
                Buckets = Histogram.ExponentialBuckets(0.001, 2, 10)
            });

        private static readonly Counter PythonApiErrorsTotal = Metrics.CreateCounter(
            "python_api_errors_total",
            "Total number of Python API errors",
            new CounterConfiguration
            {
                LabelNames = new[] { "endpoint", "error_type" }
            });

        private static readonly Gauge PythonApiActiveRequests = Metrics.CreateGauge(
            "python_api_active_requests",
            "Number of active Python API requests",
            new GaugeConfiguration
            {
                LabelNames = new[] { "endpoint" }
            });

        public PythonApiMetricsMiddleware(RequestDelegate next, ILogger<PythonApiMetricsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;
            
            // Only track Python API and gateway metrics
            if (!path.StartsWith("/api/pythoncompanies", StringComparison.OrdinalIgnoreCase) &&
                !path.StartsWith("/api/hybridcompanies", StringComparison.OrdinalIgnoreCase) &&
                !path.StartsWith("/django-api", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var method = context.Request.Method;
            var endpoint = GetEndpointName(path);
            var stopwatch = Stopwatch.StartNew();

            // Increment active requests
            PythonApiActiveRequests.WithLabels(endpoint).Inc();

            try
            {
                await _next(context);

                stopwatch.Stop();
                var duration = stopwatch.Elapsed.TotalSeconds;
                var statusCode = context.Response.StatusCode.ToString();

                // Record metrics
                PythonApiRequestsTotal.WithLabels(endpoint, method, statusCode).Inc();
                PythonApiRequestDuration.WithLabels(endpoint, method).Observe(duration);

                _logger.LogInformation(
                    "Python API request: {Method} {Endpoint} completed in {Duration}ms with status {StatusCode}",
                    method, endpoint, stopwatch.ElapsedMilliseconds, statusCode);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                var errorType = ex.GetType().Name;

                PythonApiErrorsTotal.WithLabels(endpoint, errorType).Inc();
                PythonApiRequestsTotal.WithLabels(endpoint, method, "500").Inc();

                _logger.LogError(ex,
                    "Python API request error: {Method} {Endpoint} failed after {Duration}ms",
                    method, endpoint, stopwatch.ElapsedMilliseconds);

                throw;
            }
            finally
            {
                // Decrement active requests
                PythonApiActiveRequests.WithLabels(endpoint).Dec();
            }
        }

        private static string GetEndpointName(string path)
        {
            if (path.StartsWith("/django-api", StringComparison.OrdinalIgnoreCase))
                return "django-api";
            if (path.StartsWith("/api/pythoncompanies", StringComparison.OrdinalIgnoreCase))
                return "pythoncompanies";
            if (path.StartsWith("/api/hybridcompanies", StringComparison.OrdinalIgnoreCase))
                return "hybridcompanies";
            
            return "unknown";
        }
    }
}

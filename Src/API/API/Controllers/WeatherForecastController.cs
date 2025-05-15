using Common.Logging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly  ILoggerService _logger;

        public WeatherForecastController(ILoggerService logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<String> Get()
        {
            _logger.LogInformation("Here is info message from our values controller.");
            _logger.LogDebug("Here is debug message from our values controller.");
            _logger.LogWarning("Here is warn message from our values controller.");
            _logger.LogError(new Exception(),"Here is an error message from our values controller.");
           
            return new string[] { "value1", "value2" };
        }
    }
}

using Microsoft.Extensions.Configuration;

namespace PomodoroService.Models
{
    internal class PomodoroConfig : IPomodoroConfig
    {
        private readonly IConfiguration _configuration;

        public PomodoroConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int DefaultIntervalLength => int.Parse(_configuration.GetSection("DefaultIntervalLength")?.Value);
        public int DefaultRestLength => int.Parse(_configuration.GetSection("DefaultRestLength")?.Value);
    }
}

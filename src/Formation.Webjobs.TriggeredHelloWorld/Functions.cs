using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Formation.Webjobs.TriggeredHelloWorld
{
    public class Functions
    {
        private readonly ILogger _logger;

        public Functions(ILogger<Functions> logger)
        {
            this._logger = logger;
        }

        [NoAutomaticTrigger]
        public Task HelloWorld()
        {
            this._logger.LogTrace("Hello, World!");
            return Task.CompletedTask;
        }
    }
}

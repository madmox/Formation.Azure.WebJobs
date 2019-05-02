using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Formation.Webjobs.ContinuousHelloWorld
{
    public class Functions
    {
        private readonly ILogger _logger;

        public Functions(ILogger<Functions> logger)
        {
            this._logger = logger;
        }

        public Task HelloWorld([ServiceBusTrigger(topicName: "helloworld", subscriptionName: "%helloworld%", Connection = "AzureWebJobsServiceBus")]HelloWorldMessage command)
        {
            this._logger.LogTrace(command.Message);
            return Task.CompletedTask;
        }
    }

    public class HelloWorldMessage
    {
        public string Message { get; set; }
    }
}

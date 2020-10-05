using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzFunc_with_EventHub
{
    public class EventHubTriggerFunc
    {
        [FunctionName("ServicebusQueueFunc")]
        public static void Run([EventHubTrigger("sampleeventhubad", Connection = "connectionString")] string myEvents, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myEvents}");
            //Console.WriteLine(myQueueItem);
        }
    }
}

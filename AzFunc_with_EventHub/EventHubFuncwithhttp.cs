using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace AzFunc_with_EventHub
{
    public static class EventHubFuncwithhttp
    {

        //[FunctionName("EventHubOutput")]
        //[return: EventHub("outputEventHubMessage", Connection = "EventHubConnectionAppSetting")]
        //public static string Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        //{
        //    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        //    return $"{DateTime.Now}";
        //}


        [FunctionName("EventHubFuncwithhttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [EventHub("sampleeventhubad", Connection = "connectionString")] IAsyncCollector<string> outputEvents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            await outputEvents.AddAsync(requestBody);

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

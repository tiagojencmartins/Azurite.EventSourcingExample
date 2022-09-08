using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using UserEventStorage.Models;

namespace UserEventStorage
{
    public static class UserEventStorage
    {
        [FunctionName(nameof(UserEventStorage))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "user/{id}")] HttpRequest request,
            string id,
            [Table("UserEvents", Connection = "AzureWebJobsStorage")] TableClient tableClient,
            ILogger log)
        {
            log.LogInformation($"HTTP trigger {nameof(UserEventStorage)} function processed a request.");

            try
            {
                await tableClient.CreateIfNotExistsAsync();

                var message = await new StreamReader(request.Body).ReadToEndAsync();

                var storageRequest = JsonConvert.DeserializeObject<EventStorageRequest>(message);

                var response = await tableClient.AddEntityAsync(Entity.Create(id, storageRequest.Event, storageRequest.Payload));

                if (response is null)
                {
                    log.LogError($"Error inserting {nameof(UserEventStorage)} with data: {message}");
                    return new BadRequestResult();
                }
            }
            catch
            {
                log.LogError($"Error processing {nameof(UserEventStorage)} with data: {request.Body}");
                return new InternalServerErrorResult();
            }

            return new StatusCodeResult(201);
        }
    }
}
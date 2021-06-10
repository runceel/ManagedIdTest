using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using Azure.Identity;
using Azure;
using System.Text;

namespace ManagedIdTest
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var blobName = $"{Guid.NewGuid()}.txt";
            await CreateBlockBlobAsync("stkaotaoutput", "fromfunctionapp", blobName);

            return new OkObjectResult($"{blobName} was created.");
        }

        async static Task CreateBlockBlobAsync(string accountName, string containerName, string blobName)
        {
            // Construct the blob container endpoint from the arguments.
            var containerEndpoint = $"https://{accountName}.blob.core.windows.net/{containerName}";

            // Get a credential and create a client object for the blob container.
            var containerClient = new BlobContainerClient(
                new Uri(containerEndpoint),
                new DefaultAzureCredential());

            // Create the container if it does not exist.
            await containerClient.CreateIfNotExistsAsync();

            // Upload text to a new block blob.
            var blobContents = "This is a block blob.";
            var byteArray = Encoding.ASCII.GetBytes(blobContents);

            using (var stream = new MemoryStream(byteArray))
            {
                await containerClient.UploadBlobAsync(blobName, stream);
            }
        }
    }
}

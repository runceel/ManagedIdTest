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
    public class Function1
    {
        private readonly BlobServiceClient _blobServiceClient;

        public Function1(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var containerClient = _blobServiceClient.GetBlobContainerClient("sample");
            await containerClient.CreateIfNotExistsAsync();
            var blobName = $"{Guid.NewGuid()}.txt";
            var blobContents = "This is a block blob.";
            var byteArray = Encoding.ASCII.GetBytes(blobContents);

            using (var stream = new MemoryStream(byteArray))
            {
                await containerClient.UploadBlobAsync(blobName, stream);
            }

            return new OkObjectResult($"{blobName} was created.");
        }
    }
}

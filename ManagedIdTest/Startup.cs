using Azure.Identity;
using Azure.Storage.Blobs;
using ManagedIdTest;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly:FunctionsStartup(typeof(Startup))]

namespace ManagedIdTest
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(p =>
            {
                var c = p.GetRequiredService<IConfiguration>();
                return new BlobServiceClient(new Uri(c["BLOB_URL"]), new DefaultAzureCredential());
            });
        }
    }
}

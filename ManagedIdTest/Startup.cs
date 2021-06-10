using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedIdTest
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);
            builder.ConfigurationBuilder.AddUserSecrets<Startup>();
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace MSSecurity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilderContext, builder) => {
                    //var builtConfig = builder.Build();

                    //builder.AddAzureKeyVault(
                    //    $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                    //    builtConfig["AzureApplicationId"],
                    //    builtConfig["AzureSecretClient"]);

                    /**AÑADIR LA CONFIG DE SPRING CLOUD**/
                    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
                    builder.AddConfigServer(hostingEnvironment.EnvironmentName);
                })
                .UseStartup<Startup>();
    }
}

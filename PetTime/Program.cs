using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PetTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if ((args.Length > 0) && (args[0].ToLowerInvariant() == "scrape"))
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>();

                IConfigurationRoot configuration = builder.Build();

                Services.DataScraper scraper = new Services.DataScraper(configuration.GetValue<string>("Dog.ApiKey"));
                scraper.Scrape();
            }
            else
            {
                BuildWebHost(args).Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

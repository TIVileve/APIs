using System;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Vileve.Services.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .ConfigureLogging((ctx, logging) =>
                {
                    logging.AddElmahIo(options =>
                    {
                        options.ApiKey = "d84faef876234265bfbed47bd6012bcc";
                        options.LogId = new Guid("cf7129aa-e399-43a6-b732-2ea3f8f7f82b");
                    });
                    logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
                })
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
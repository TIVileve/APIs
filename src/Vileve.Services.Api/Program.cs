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
                        options.ApiKey = "74f193118fec49c5a22d9d3659f37c6e";
                        options.LogId = new Guid("0245a3f7-5d90-4318-83d2-af17d29a3c0c");
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
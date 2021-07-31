﻿using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Silky.StockHost
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .RegisterSilkyServices<StockHostModule>()
                .UseSerilogDefault();
        }
    }
}
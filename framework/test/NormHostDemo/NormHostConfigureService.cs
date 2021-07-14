﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NormHostDemo.Contexts;
using Silky.Lms.Core;

namespace NormHostDemo
{
    public class NormHostConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DemoDbContext>();
            },"NormHostDemo");
        }

        public int Order { get; } = 10;
    }
}
﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.Core.Modularity;

namespace Silky.Caching.StackExchangeRedis
{
    [DependsOn(typeof(CachingModule))]
    public class RedisCachingModule : SilkyModule
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisEnabled = configuration["DistributedCache:Redis:IsEnabled"];

            if (redisEnabled.IsNullOrEmpty() || bool.Parse(redisEnabled))
            {
                var redisCacheOptions = configuration.GetSection("DistributedCache:Redis").Get<RedisCacheOptions>();
                if (redisCacheOptions == null || redisCacheOptions.Configuration.IsNullOrEmpty())
                {
                    throw new SilkyException("You must specify the Configuration of the redis service");
                }

                services.AddRedisCaching(redisCacheOptions);
            }
        }
    }
}
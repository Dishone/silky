using Consul;
using Microsoft.Extensions.Options;
using Silky.RegistryCenter.Consul.Configuration;

namespace Silky.RegistryCenter.Consul
{
    public class ConsulClientFactory : IConsulClientFactory
    {
        private readonly IOptionsMonitor<ConsulClientConfiguration> _optionsMonitor;

        public ConsulClientFactory(IOptionsMonitor<ConsulClientConfiguration> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        public IConsulClient CreateClient()
        {
            return new ConsulClient(_optionsMonitor.CurrentValue);
        }
    }
}
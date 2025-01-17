using Silky.Rpc.Configuration;

namespace Silky.RegistryCenter.Zookeeper.Configuration
{
    public class ZookeeperRegistryCenterOptions : IRegistryCenterOptions
    {
        internal static string RegistryCenter = "RegistryCenter";

        public string Type { get; } = "Zookeeper";
        public double ConnectionTimeout { get; set; } = 5000;

        public double SessionTimeout { get; set; } = 8000;

        public double OperatingTimeout { get; set; } = 10000;

        public string ConnectionStrings { get; set; }

        public int FuseTimes { get; set; } = 10;

        public string RoutePath { get; set; } = "/silky/server";
    }
}
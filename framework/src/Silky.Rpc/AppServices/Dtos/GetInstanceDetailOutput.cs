using System;
using Silky.Rpc.Runtime.Client;
using Silky.Rpc.Runtime.Server;

namespace Silky.Rpc.AppServices.Dtos
{
    public class GetInstanceDetailOutput
    {
        public string Address { get; set; }

        public string HostName { get; set; }

        public DateTime StartTime { get; set; }

        public ServiceInstanceHandleInfo HandleInfo { get; set; }

        public ServiceInstanceInvokeInfo InvokeInfo { get; set; }
    }
}
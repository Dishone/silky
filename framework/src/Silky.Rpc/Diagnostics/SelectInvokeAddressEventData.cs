using Silky.Rpc.Endpoint;
using Silky.Rpc.Endpoint.Selector;

namespace Silky.Rpc.Diagnostics
{
    public class SelectInvokeAddressEventData
    {
        public IRpcEndpoint[] EnableRpcEndpoints { get; set; }
        public IRpcEndpoint SelectedRpcEndpoint { get; set; }
        public ShuntStrategy ShuntStrategy { get; set; }
        public string ServiceEntryId { get; set; }
    }
}
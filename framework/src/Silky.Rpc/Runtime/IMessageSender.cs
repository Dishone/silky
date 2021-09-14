using System.Threading.Tasks;
using Silky.Rpc.Messages;

namespace Silky.Rpc.Runtime
{
    public interface IMessageSender
    {
        Task SendMessageAsync(TransportMessage message, bool flush = true);
    }
}
using System.Linq;
using Silky.Core.Exceptions;
using Silky.Rpc.Messages;
using ProtoBuf;

namespace Silky.Codec.Message
{
    [ProtoContract]
    public class ProtoBufRemoteResultMessage
    {
        public ProtoBufRemoteResultMessage(RemoteResultMessage remoteResultMessage)
        {
            ExceptionMessage = remoteResultMessage.ExceptionMessage;
            StatusCode = remoteResultMessage.StatusCode;
            Result = remoteResultMessage.Result == null ? null : new DynamicItem(remoteResultMessage.Result);
            ValidateErrors = remoteResultMessage.ValidateErrors?.Select(i => new DynamicItem(i)).ToArray();
        }

        public ProtoBufRemoteResultMessage()
        {
        }

        [ProtoMember(1)] public string ServiceId { get; set; }
        [ProtoMember(2)] public string ExceptionMessage { get; set; }

        [ProtoMember(3)] public StatusCode StatusCode { get; set; } = StatusCode.Success;

        [ProtoMember(4)] public DynamicItem Result { get; set; }

        [ProtoMember(5)] public DynamicItem[] ValidateErrors { get; set; }

        public RemoteResultMessage GetMessage()
        {
            return new()
            {
                ServiceId = ServiceId,
                ExceptionMessage = ExceptionMessage,
                StatusCode = StatusCode,
                Result = Result?.Get(),
                ValidateErrors = ValidateErrors.Select(p => (ValidError) p.Get()).ToArray()
            };
        }
    }
}
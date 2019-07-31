using Surging.Core.Protocol.Mqtt.Internal.Messages;
using System.Collections.Concurrent;

namespace Surging.Core.Protocol.Mqtt.Internal.Services
{
    public interface IClientSessionService
    {
        void SaveMessage(string deviceId, SessionMessage sessionMessage);

        ConcurrentQueue<SessionMessage> GetMessages(string deviceId);
    }
}
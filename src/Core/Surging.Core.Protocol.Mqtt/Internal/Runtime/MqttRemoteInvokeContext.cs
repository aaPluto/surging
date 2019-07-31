using Surging.Core.CPlatform.Runtime.Client;

namespace Surging.Core.Protocol.Mqtt.Internal.Runtime
{
    public class MqttRemoteInvokeContext : RemoteInvokeContext
    {
        public string topic { get; set; }
    }
}
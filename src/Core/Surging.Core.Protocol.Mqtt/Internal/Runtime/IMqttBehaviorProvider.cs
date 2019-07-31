using Surging.Core.Protocol.Mqtt.Internal.Services;

namespace Surging.Core.Protocol.Mqtt.Internal.Runtime
{
    public interface IMqttBehaviorProvider
    {
        MqttBehavior GetMqttBehavior();
    }
}
using Surging.Core.CPlatform;

namespace Surging.Core.Codec.ProtoBuffer
{
    public static class ContainerBuilderExtensions
    {
        public static IServiceBuilder UseProtoBufferCodec(this IServiceBuilder builder)
        {
            return builder.UseCodec<ProtoBufferTransportMessageCodecFactory>();
        }
    }
}
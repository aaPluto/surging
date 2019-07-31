using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Core.WsProxy
{
    public class MessageNotifyProxy : IMessageNotifyProxy
    {
        public async Task<NotifySymbol> NotifyMessage<T>(MessageInfo<T> messageInfo) where T : class
        {
            string notifyApi;
            string connIdName;
            var messageModelName = "message";
            switch (messageInfo.MessageType)
            {
                case MessageType.LineNorm:
                    notifyApi = NotifyApi.LineNormNotifyApi;
                    connIdName = "connId";
                    break;
                default:
                    throw new BusinessException("不存在该类型的消息");
            }

            var rpcParams = new Dictionary<string, object>
            {
                {connIdName, messageInfo.ConnId}, {messageModelName, messageInfo.Model}
            };
            return await ServiceLocator.GetService<IServiceProxyProvider>()
                .Invoke<NotifySymbol>(rpcParams, notifyApi, NotifyApi.ServiceKey.V1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Core.WsProxy
{
    public class MessageInfo<T> where T : class
    {
        public MessageInfo(MessageType messageType, string connId, T model)
        {
            MessageType = MessageType;
            ConnId = connId;
            Model = model;
        }

        public MessageInfo(MessageType messageType, string connId)
        {
            MessageType = MessageType;
            ConnId = connId;
        }

        public MessageType MessageType { get; }

        public string ConnId { get; }

        public T Model { get; set; }
    }
}

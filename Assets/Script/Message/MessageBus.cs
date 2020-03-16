using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus
{
    private static MessageBus _bus;
    public static MessageBus GetBus()
    {
        if (_bus == null)
            _bus = new MessageBus();
        return _bus;
    }

    private readonly List<IMessageHandler> Handlers = new List<IMessageHandler>();
    
    public void Subscribe(IMessageHandler handler)
    {
        Handlers.Add(handler);
    }
    public void SendMessage(MessageModel model)
    {
        foreach (var item in Handlers)
        {
            item.Handle(model);
        }
    }
}

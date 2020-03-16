using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MessageModel
{
    public object Parametr { get; }
    public MessageType Type { get; }

    public MessageModel(object obj,MessageType type)
    {
        Parametr = obj;
        Type = type;
    }
}

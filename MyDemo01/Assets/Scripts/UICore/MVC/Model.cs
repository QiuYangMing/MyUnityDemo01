using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//模型层，用于存放数据
public abstract class Model
{
    //模型名称
    public abstract string Name { get; }
    //发送消息
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}

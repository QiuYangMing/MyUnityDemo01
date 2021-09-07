using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC
{

    //存储MVC三层
    //名称--模型
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
    //名称--视图
    public static Dictionary<string, View> Views = new Dictionary<string, View>();
    //事件名--控制器类型
    public static Dictionary<string, Type> CommandDic = new Dictionary<string, Type>();
    //以下是注册相关-------------------------------
    //注册模型
    public static void RegisterModel(Model model)
    {
        if (!Models.ContainsKey(model.Name))
        {
            Models.Add(model.Name, null);
        }
        Models[model.Name] = model;
    }
    //注册视图
    public static void RegisterView(View view)
    {
        if (!Views.ContainsKey(view.Name))
        {
            Views.Add(view.Name, null);
        }
        view.RegisterEvents();
        Views[view.Name] = view;

    }
    //注册控制器
    public static void RegisterController(string eventName, Type ctrlType)
    {
        if (!CommandDic.ContainsKey(eventName))
        {
            CommandDic.Add(eventName, null);
        }
        CommandDic[eventName] = ctrlType;
    }

    //以下是获取相关-------------------------------
    //获取模型
    public static T GetModel<T>() where T : Model
    {
        foreach (Model model in Models.Values)
        {
            if (model is T)
            {
                return (T)model;
            }
        }
        return null;
    }
    //获取视图
    public static T GetView<T>() where T : View
    {
        foreach (View view in Views.Values)
        {
            if (view is T)
            {
                return (T)view;
            }
        }
        return null;
    }
    //发送事件(触发)
    public static void SendEvent(string eventName, object data = null)
    {
        //控制器响应事件
        if (CommandDic.ContainsKey(eventName))
        {
            Type t = CommandDic[eventName];
            //Activator.CreateInstance创建一个泛型参数所属类型的对象
            Controller ctrl = (Controller)Activator.CreateInstance(t);
            //执行控制器里面对应的方法
            ctrl.Execute(data);
        }
        //视图响应事件
        foreach (View view in Views.Values)
        {
            if (view.AttentionEvents.Contains(eventName))
            {
                view.HandEvent(eventName, data);
            }
        }
    }
}

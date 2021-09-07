using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Controller
{

    //控制器对模型的引用
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>();
    }
    //控制器对视图的引用
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>();
    }
    //以下是注册-------------
    //注册模型
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    //注册视图
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    //注册控制器（自身）
    protected void RegisterController(string eventName, Type ctrlType)
    {
        MVC.RegisterController(eventName, ctrlType);
    }
    //监听到消息后处理某些逻辑
    public abstract void Execute(object data);
}

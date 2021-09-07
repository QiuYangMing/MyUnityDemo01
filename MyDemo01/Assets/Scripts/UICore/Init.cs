using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    void Start()
    {
        //注册控制器（命令与控制器进行绑定）
        MVC.RegisterController(GameDefine.command_Init, typeof(InitCtrl));
        //发送命令（启动框架）
        MVC.SendEvent(GameDefine.command_Init);
    }
}

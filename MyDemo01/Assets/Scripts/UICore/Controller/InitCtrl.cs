using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCtrl : Controller
{
    public override void Execute(object data)
    {
        //初始化项目
        //判断主场景是否第一次被加载，如果是，就要生成一个画布
        if (GameTool.isFirseLoad)
        {
            //注册模型
            RegisterAllModel();
            //注册视图
            //RegisterAllView();
            //注册控制器
            RegisterAllController();
            //初始化模型数据    
            InitAllModel();

            //加载配置表
            //DataController.Instance.LoadAllCfg();
            GameObject canvasPrefab = Resources.Load<GameObject>("UIPrefab/Canvas");
            GameObject canvas = GameObject.Instantiate(canvasPrefab);
            GameTool.isFirseLoad = false;
            InitManager(canvas);
        }
    }
    //注册所有的模型
    private void RegisterAllModel()
    {
        RegisterModel(new InforData());
        RegisterModel(new ShopData());
        RegisterModel(new EnInforData());
    }
    //注册所有的控制器(其实就是命令与控制器进行绑定，如果没有绑定，那么控制器里面的Execute是不会被执行的)
    private void RegisterAllController()
    {
       RegisterController(GameDefine.command_AddHP, typeof(Com_UpdateHP));
       RegisterController(GameDefine.command_AddRedORB, typeof(Com_UpdateRedORB));
       RegisterController(GameDefine.command_EnAddHP, typeof(Com_EnUpdataHp));

    }
    //初始化所有模型数据
    private void InitAllModel()
    {
        GetModel<InforData>().InitInforData();
        GetModel<ShopData>().InitShopData();
    }
    private void InitManager(GameObject canvas)
    {

        GameTool.AddTheChildComponent<UIManager>(canvas, "UnitySingletonObj");
        GameTool.AddTheChildComponent<GameSceneManager>(canvas, "UnitySingletonObj");
        GameTool.AddTheChildComponent<AudioManager>(canvas, "UnitySingletonObj");
        GameTool.AddTheChildComponent<AttackScene>(canvas, "UnitySingletonObj");


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//物品的类型
public enum E_GoodsType
{
    Default,//全部0
    Equipment,//装备1
    Potins,//药水2
    Rune,//符文3
    Material//材料4
}
//物品出现的地方(UI界面)
//消息类型
public enum E_MessageType
{
    UpdateCoin,//出售物体
    ChooseGoods,
    ChooseGoodsExchange,//兑换物品被选中
}
//窗体的ID
public enum E_UiId
{
    NullUI = 0,
    MainUI,
    InforUI,
    ShopUI,
    LoadingUI,
    ExitUI,
    ExchangeUI,
    AudioUI,
    WoundedUI,
    ResetGameUI,
    EnemyInforUI,
    StopUI
}
//窗体的显示方式
public enum E_ShowUIMode
{
    //界面显示出来的时候，不需要去隐藏其他窗体（比如主界面）
    DoNothing,
    //界面显示出来的时候，需要隐藏其他窗体，但是不会隐藏保持在最前方的窗体（比如关卡界面不会隐藏信息界面）
    HideOhter,
    //界面显示出来的时候，需要隐藏所有的窗体,包括保持在最前方的窗体也要去隐藏它（比如PlayUI显示出来，要隐藏所有界面）
    HideAll
}
public enum E_GoodsUiType
{
    Default,
    Pack,
    Exchange
}
//窗体的层级类型（父节点的类型）
public enum E_UIRootType
{
    KeepAbove,//保持在最前方的窗体（DoNothing）
    Normal//普通窗体（1、HideOhter 2、HideAll）
}
public class GameDefine
{
    //定义命令
    public const string command_Init = "Command_Init";
    public const string command_AddHP = "Command_AddHp";
    public const string command_EnAddHP = "Command_EnAddHp";
    public const string command_AddRedORB = "Command_AddRedORB";

    //定义消息
    public const string message_UpdatePoint = "Message_UpdatePoint";
    public const string message_EnUpdatePoint = "Message_EnUpdatePoint";
    public const string message_UpdateRedORB = "Message_UpdateRedORB";
    public const string message_UpdateGreenORB = "Message_UpdateGreenORB";
    public const string message_UpdateBlueORB = "Message_UpdateBlueORB";
    public const string message_UpdateSkill1 = "Message_UpdateSkill1";
    public const string message_UpdateSkill2 = "Message_UpdateSkill2";
    public const string message_UpdateSkill3 = "Message_UpdateSkill3";



    //窗体的加载路径<窗体ID,加载路径>
    public static Dictionary<E_UiId, string> dicPath = new Dictionary<E_UiId, string>()
    {
            {E_UiId.MainUI,"UIPrefab/"+"MainUI"},
            {E_UiId.InforUI,"UIPrefab/"+"InforUI"},
            {E_UiId.LoadingUI,"UIPrefab/"+"LoadingUI"},
            {E_UiId.ExitUI,"UIPrefab/"+"ExitUI"},
            {E_UiId.ExchangeUI,"UIPrefab/"+"ExchangeUI"},
            {E_UiId.AudioUI,"UIPrefab/"+"AudioUI"},
            { E_UiId.ShopUI,"UIPrefab/"+"ShopUI"},
            { E_UiId.WoundedUI,"UIPrefab/"+"WoundedUI"},
            { E_UiId.ResetGameUI,"UIPrefab/"+"ResetGameUI"},
            { E_UiId.EnemyInforUI,"UIPrefab/"+"EnemyInforUI"},
            { E_UiId.StopUI,"UIPrefab/"+"StopUI"}




    };
    public static Type GetUIScriptType(E_UiId uiId)
    {
        Type scriptType = null;
        switch (uiId)
        {
            case E_UiId.NullUI:
                Debug.LogError("添加脚本的时候，传入的窗体ID为NullUI");
                break;
            case E_UiId.MainUI:
                scriptType = typeof(MainUI);
                break;
            case E_UiId.InforUI:
                scriptType = typeof(InforUI);
                break;
            case E_UiId.ShopUI:
                scriptType = typeof(ShopUI);
                break;
            case E_UiId.LoadingUI:
                scriptType = typeof(LoadingUI);
                break;
            case E_UiId.ExitUI:
                scriptType = typeof(ExitUI);
                break;
            case E_UiId.WoundedUI:
                scriptType = typeof(WoundedUI);
                break;
            case E_UiId.AudioUI:
                scriptType = typeof(AudioUI);
                break;
            case E_UiId.ResetGameUI:
                scriptType = typeof(ResetGameUI);
                break;
            case E_UiId.EnemyInforUI:
                scriptType = typeof(EnemyInforUI);
                break;
            case E_UiId.StopUI:
                scriptType = typeof(StopUI);
                break;
            default:
                break;
        }
        return scriptType;
    }

}

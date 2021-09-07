using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI基类
//主要封装窗体的相同属性以及行为
//视图层，用于显示数据，接收用户输入
//窗体的类型
public class UIType
{
    //显示类型
    public E_ShowUIMode showMode = E_ShowUIMode.HideOhter;
    //层级类型（父节点类型）
    public E_UIRootType uiRootType = E_UIRootType.Normal;
}

public abstract class View : MonoBehaviour {
   //----------------这里是MVC重的View主逻辑
    //视图的名称
    public abstract string Name { get; }
    //存储视图所监听的消息
    [HideInInspector]
    public List<string> AttentionEvents = new List<string>();
    //监听关心的消息
    public virtual void RegisterEvents()
    {

    }
    //监听到消息后所要处理的逻辑
    public abstract void HandEvent(string eventName, object data);
    //视图对模型的引用
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>();
    }
    //发送命令
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
    //注册视图
    protected void RegisterView()
    {
        MVC.RegisterView(this);
    }
    
   
    //----------------这里是UI窗体主逻辑
    //窗体类型
    public UIType uiType;
    //窗体的RectTransform
    protected RectTransform thisTrans;
    //当前窗体的ID
    protected E_UiId uiId = E_UiId.NullUI;
    //上一个跳转过来的窗体ID
    protected E_UiId beforeUiId = E_UiId.NullUI;
    //供外界访问的，获取当前窗体的ID
    public E_UiId GetUiId
    {
        get
        {
            return uiId;
        }
        //为什么没有set？因为每一个窗体的ID是固定的，只能让外界去获取，而不能随意修改
    }
    public E_UiId GetBeforeUiId
    {
        get
        {
            return beforeUiId;
        }
        set
        {
            beforeUiId = value;
        }
    }
    //对外提供一个属性，用来判断显示出来是否要处理其他窗体的隐藏
    public bool IsNeedDealWithUI
    {
        get
        {
            if (uiType.uiRootType == E_UIRootType.KeepAbove)
            {
                //保持在最前方的窗体，显示出来的时候不需要隐藏其他窗体
                return false;
            }
            else
            {
                //显示出来的时候，需要隐藏其他窗体
                return true;
            }
        }
    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void Awake()
    {
        if (uiType == null)
        {
            uiType = new UIType();
        }
        thisTrans = this.GetComponent<RectTransform>();
        //初始化界面元素
        InitUiOnAwake();
        //初始化界面数据
        InitDataOnAwake();
        //注册视图
        RegisterView();

    }
    //初始化界面元素
    protected virtual void InitUiOnAwake()
    {
        //比如查找按钮，给按钮添加监听
    }
    //初始化界面数据
    protected virtual void InitDataOnAwake()
    {
        //比如界面的ID,窗体类型
    }
    //窗体显示
    public virtual void ShowUI()
    {

        this.gameObject.SetActive(true);
    }
    //隐藏窗体
    public virtual void HideUI(Action del = null)
    {
        this.gameObject.SetActive(false);
        if (del != null)
        {
            del();
        }
        //保存数据
        Save();
    }
    protected virtual void Update()
    {

    }
    protected virtual void Save()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void OnDisable()
    {

    }
    protected virtual void OnDestroy()
    {

    }
}

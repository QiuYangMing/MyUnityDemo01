using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{

    //缓存所有打开过的窗体
    private Dictionary<E_UiId, View> dicAllUI;
    //缓存所有正在显示的窗体
    private Dictionary<E_UiId, View> dicShowUI;
    //缓存最近一个显示出来的窗体
    private View currentUI = null;
    //缓存上一个跳转过来的窗体
    private View beforeUI = null;
    private E_UiId beforeUiId = E_UiId.NullUI;
    //缓存画布（整个项目共用一个画布）
    private Transform canvas;
    //缓存保持在最前方窗体的父节点
    private Transform keepAboveUIRoot;
    //范村普通窗体的父节点
    private Transform normalUIRoot;
    private void Awake()
    {
        dicAllUI = new Dictionary<E_UiId, View>();
        dicShowUI = new Dictionary<E_UiId, View>();
        InitUIManager();
    }
    private void InitUIManager()
    {
        canvas = this.transform.parent;
        keepAboveUIRoot = GameTool.FindTheChild(canvas.gameObject, "KeepAboveUIRoot");
        normalUIRoot = GameTool.FindTheChild(canvas.gameObject, "NormalUIRoot");
        //场景切换的时候，不销毁画布
        DontDestroyOnLoad(canvas);
        //开始显示UI界面
        ShowUI(E_UiId.MainUI);
        //ShowUI(E_UiId.InforUI);
    }
    //对外提供的，显示窗体的方法
    public void ShowUI(E_UiId uiId, bool isNeedSaveBeforeUiId = true)
    {
        if (uiId == E_UiId.NullUI)
        {
            uiId = E_UiId.MainUI;
        }
        View baseUI = JudgeUI(uiId);
        if (baseUI != null)
        {
            baseUI.ShowUI();
            if (isNeedSaveBeforeUiId)
            {
                //存储
                baseUI.GetBeforeUiId = beforeUiId;
            }
        }

    }
    //对外提供的，窗体反向切换的方法（点击界面返回按钮）
    public void ReturnUI(E_UiId uiId)//传入上一个窗体的ID
    {
        ShowUI(uiId, false);
    }

    private View JudgeUI(E_UiId uiId)
    {
        //判断窗体是否正在显示
        if (dicShowUI.ContainsKey(uiId))
        {
            //要显示的窗体已经正在显示了，所以就直接跳出该方法
            return null;
        }
        //判断窗体是否有加载显示过
        View baseUI = GetView(uiId);
        if (baseUI == null)//说明窗体没有加载显示过
        {
            //要去动态加载窗体的预制体
            //获取加载路径
            string path = GameDefine.dicPath[uiId];
            //通过对应的路径把窗体加载进来
            GameObject theUI = Resources.Load<GameObject>(path);
            if (theUI != null)
            {
                //把窗体生成出来
                GameObject willShowUI = Instantiate(theUI);
                //判断该窗体上面有没有UI脚本
                baseUI = willShowUI.GetComponent<View>();
                if (baseUI == null)  //如果没有，就要自动添加
                {
                    Type type = GameDefine.GetUIScriptType(uiId);
                    //为窗体自动添加脚本
                    baseUI = willShowUI.AddComponent(type) as View;
                }
                //获取该窗体对应的UI根节点
                Transform uiRoot = GetUIRoot(baseUI);
                GameTool.AddChildToParent(uiRoot, willShowUI.transform);
                willShowUI.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                willShowUI.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                //窗体第一次显示出来，就要进行缓存
                dicAllUI.Add(uiId, baseUI);
            }
            else
            {
                Debug.LogError("在路径" + path + "下面加载不到窗体，请查看该路径下面是否有该窗体的预制体");
            }

        }
        UpdateDicAndHideUI(baseUI);
        return baseUI;
    }
    //当有窗体显示出来的时候，隐藏对应的UI界面,更新dicShowUI这个字典
    private void UpdateDicAndHideUI(View baseUI)
    {
        if (baseUI.IsNeedDealWithUI)
        {
            //进入到这个条件，说明这个窗体是普通窗体，需要隐藏其他窗体
            //隐藏的方式有两种：1：隐藏不包括保持在最前方的窗体 2、隐藏所有的窗体
            if (dicShowUI.Count > 0)
            {
                if (baseUI.uiType.showMode == E_ShowUIMode.HideAll)
                {
                    //隐藏所有的窗体
                    HideAllUI(true, baseUI);
                }
                else //HideOther  DoNothing
                {
                    //隐藏不包括保持在最前方的窗体,注意要排除掉DoNothing
                    HideAllUI(false, baseUI);
                }
            }
        }
        dicShowUI.Add(baseUI.GetUiId, baseUI);
    }
    //对外提供的，隐藏单个窗体的方法
    public void HideSingleUI(E_UiId uiId, Action del = null)
    {
        if (!dicShowUI.ContainsKey(uiId))
        {
            //说明要隐藏的窗体没有显示，所有不需要处理其他逻辑
            return;
        }
        //一下是需要隐藏的情况
        dicShowUI[uiId].HideUI();//隐藏窗体
        dicShowUI.Remove(uiId);
    }
    //对外提供的，隐藏所有窗体的方法
    public void HideAllUI(bool isNeedHideAboveUI, View baseUI)
    {
        if (isNeedHideAboveUI) //隐藏所有的窗体
        {
            foreach (View uiItem in dicShowUI.Values)
            {
                uiItem.HideUI();
            }
            dicShowUI.Clear();
        }
        else //隐藏不包括保持在最前方的窗体
        {
            //排除DoNothing，不需要隐藏其他窗体，直接跳出方法
            if (baseUI.uiType.showMode == E_ShowUIMode.DoNothing)
            {
                return;
            }
            //以下是HideOther的情况
            //存储将要在DicShowUI这个字典里面去移除的窗体
            List<E_UiId> listRemove = new List<E_UiId>();
            foreach (View uiItem in dicShowUI.Values)
            {
                //如果是保持在最前方的窗体，则不需要隐藏
                if (uiItem.uiType.uiRootType == E_UIRootType.KeepAbove)
                {
                    continue;
                }
                else
                {
                    //不是保持在最前方的窗体，把它隐藏掉
                    uiItem.HideUI();
                    //缓存上一个窗体的ID
                    // baseUI.GetBeforeUiId = uiItem.GetUiId;
                    beforeUiId = uiItem.GetUiId;
                    //Debug.Log("上一个窗体的ID为" + beforeUiId);
                    listRemove.Add(uiItem.GetUiId);
                }
            }
            for (int i = 0; i < listRemove.Count; i++)
            {
                //移除
                dicShowUI.Remove(listRemove[i]);
            }
        }
    }
    //判断窗体的UIRoot类型
    public Transform GetUIRoot(View baseUI)
    {
        if (baseUI.uiType.uiRootType == E_UIRootType.KeepAbove)
        {
            return keepAboveUIRoot;
        }
        else
        {
            return normalUIRoot;
        }
    }
    //判断窗体是否有加载显示过
    private View GetView(E_UiId uiId)
    {
        if (dicAllUI.ContainsKey(uiId))
        {
            //说明该窗体已经有加载显示过了
            return dicAllUI[uiId];
        }
        else
        {
            //说明该窗体没有加载显示过了
            return null;
        }
    }
}

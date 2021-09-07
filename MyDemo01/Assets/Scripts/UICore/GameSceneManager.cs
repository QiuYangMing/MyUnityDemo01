using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : UnitySingleton<GameSceneManager>
{

    //用于显示加载进度的进度条
    Slider slider_Progress;
    //异步加载的对象
    AsyncOperation asyn;
    //加载场景的实际进度
    private int infactProgress = 0;
    //用来显示的进度（在进度条上面显示）
    private int showProgress = 0;
    private Action asynDel = null;
    //对外提供的，异步加载场景的方法
    public void LoadNextSceneAsyn(string sceneName, Action del = null)
    {
        asynDel = del;
        //重置两个进度值
        infactProgress = 0;
        showProgress = 0;
        UIManager.Instance.ShowUI(E_UiId.LoadingUI);
        LoadNextScene("LoadingScene");
        if (slider_Progress == null)
        {
            slider_Progress = GameObject.Find("Slider_Progress").GetComponent<Slider>();
        }
        slider_Progress.value = 0;
        //开始异步加载(StartCoroutine开启协程)
        StartCoroutine(LoadSceneAsyn(sceneName));
    }
    //IEnumerator迭代器
    private IEnumerator LoadSceneAsyn(string sceneName)
    {
        //执行场景的异步加载
        asyn = SceneManager.LoadSceneAsync(sceneName);
        //场景开始加载的时候，先不显示
        asyn.allowSceneActivation = false;

        yield return asyn;

    }
    //对外提供的，直接加载场景的方法
    public void LoadNextScene(string sceneName, Action del = null)
    {
        SceneManager.LoadScene(sceneName);
        if (del != null)
        {
            del();
        }
    }
    void Update()
    {
        if (asyn == null)
        {
            return;
        }
        //asyn.progress的范围是0~1，但是要注意，这个值在代码里面顶多检测到0.9
        if (asyn.progress < 0.9f)
        {
            //场景未加载完成
            infactProgress = (int)asyn.progress * 100;
        }
        else
        {
            //默认场景加载完成了
            infactProgress = 100;
        }
        if (showProgress < infactProgress)
        {
            showProgress++;
        }
        slider_Progress.value = showProgress / 100f;
        if (showProgress == 100)
        {
            asyn.allowSceneActivation = true;
            UIManager.Instance.HideSingleUI(E_UiId.LoadingUI);
        }
       
        if (asyn.isDone && showProgress == 100)
        {
            //加载完成后需要处理一些逻辑
            if (asynDel != null)
            {
                asynDel();
            }
            asynDel = null;

            GameTool.ClaerMemory();
            asyn = null;
        }
    }
}


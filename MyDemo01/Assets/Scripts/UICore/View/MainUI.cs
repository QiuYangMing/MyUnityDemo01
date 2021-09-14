using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainUI : View
{
    //写一个UI脚本的步骤
    //1、UI脚本要继承于View
    //2、重写两个重要的方法InitUiOnAwake 与InitDataOnAwake
    //3、InitUiOnAwake主要是初始化UI元素，比如获取按钮，监听按钮单击等等
    //4、InitDataOnAwake主要是给该窗体的ID赋值以及设置父节点与显示方式
    //5、注意：重写了以上两个方法以后，在InitDataOnAwake一定要给该窗体的ID赋值
    private Button btn_StartGame;
    private Button btn_ExitGame;
    private Button btn_Aduio;
    private Button btn_ClearArchive;
    private Button btn_Level1;
    private Button btn_Level2;
    private Button btn_Level3;


    private AudioManager audioM;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
       
        btn_StartGame = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_StartGame");
        btn_ExitGame = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_ExitGame");
        btn_Aduio = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Aduio");
        btn_ClearArchive = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_ClearArchive");
        btn_Level1 = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Level1");
        btn_Level2 = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Level2");
        btn_Level3 = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Level3");
        btn_Level1.onClick.AddListener(JumpLevel1);
        btn_Level2.onClick.AddListener(JumpLevel2);
        btn_Level3.onClick.AddListener(JumpLevel3);
        btn_Aduio.onClick.AddListener(GotoAudioUI);
        btn_ExitGame.onClick.AddListener(GotoExitUI);
        btn_StartGame.onClick.AddListener(EnterLevel);
        btn_ClearArchive.onClick.AddListener(ClearArchive);
    }
    protected override void Start()
    {
        audioM = GameObject.Find("UnitySingletonObj").GetComponent<AudioManager>();
       
    }
    protected override void Update()
    {
        if (GameData.leve1Enter == 1)
        {
            btn_Level1.gameObject.SetActive(true);
        }
        else
        {
            btn_Level1.gameObject.SetActive(false);
        }
        if (GameData.leve2Enter == 1)
        {
            btn_Level2.gameObject.SetActive(true);
        }
        else
        {
            btn_Level2.gameObject.SetActive(false);
        }
        if (GameData.leve3Enter == 1)
        {
            btn_Level3.gameObject.SetActive(true);
        }
        else
        {
            btn_Level3.gameObject.SetActive(false);
        }
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    protected override void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    protected override void OnDisable()
    {
        Cursor.visible = false;
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        //每个UI脚本都必须给窗体的ID赋值，必备操作！！
        uiId = E_UiId.MainUI;
    }
  
    public override string Name
    {
        get
        {
            return uiId.ToString();
        }
    }

    public override void HandEvent(string eventName, object data)
    {
        
    }
    private void GotoAudioUI()
    {
        UIManager.Instance.ShowUI(E_UiId.AudioUI);
    }
    private void GotoExitUI()
    {
        UIManager.Instance.ShowUI(E_UiId.ExitUI);
    }
    private void JumpLevel1()
    {
        GameData.leveName = "level4Enemy";
        audioM.PlayMusic(2);
        GameSceneManager.Instance.LoadNextSceneAsyn("Level04", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI);
        });
    }
    private void JumpLevel2()
    {
        GameData.leveName = "level2Enemy";
        audioM.PlayMusic(4);
        GameSceneManager.Instance.LoadNextSceneAsyn("Level02", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI);
        });
    }
    private void JumpLevel3()
    {
        GameData.leveName = "level3Enemy";
        audioM.PlayMusic(6);
        GameSceneManager.Instance.LoadNextSceneAsyn("Level03", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI);
        });
    }
    private void EnterLevel()
    {
        GameData.leveName = "level4Enemy";
        audioM.PlayMusic(2);
        GameSceneManager.Instance.LoadNextSceneAsyn("Level04",delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI);
            GameTool.SetInt("Leve1Enter", 1);
            GameData.leve1Enter = GameTool.GetInt("Leve1Enter");
            if (GameData.firstInBaseOperation)
            {
                MVC.SendEvent(GameDefine.command_UpdateTips, "BO01");
                GameTool.SetInt("firstInBaseOperation", 0);
                GameData.firstInBaseOperation = false;
            }
        });
    }
    private void ClearArchive()
    {
        GameTool.DeleteAll();
        UIManager.Instance.ShowUI(E_UiId.ClearArchivesTipsUI);
    }
}

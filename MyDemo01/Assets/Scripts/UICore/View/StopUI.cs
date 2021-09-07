using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopUI : View
{
    private Button btn_ReturnMainUI;
    private Button btn_AgainGame;
    private Button btn_BreakGame;
    private AudioManager audioM;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        audioM = GameObject.Find("UnitySingletonObj").GetComponent<AudioManager>();
        btn_ReturnMainUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ReturnMainUI");
        btn_AgainGame = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_AgainGame");
        btn_BreakGame = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_BreakGame");
        btn_AgainGame.onClick.AddListener(OnResetGame);
        btn_ReturnMainUI.onClick.AddListener(OnReturnMain);
        btn_BreakGame.onClick.AddListener(OnBreakGame);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.StopUI;
        uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    protected override void OnEnable()
    {
        Time.timeScale = 0;
        audioM.PlayOrPauseMusic(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    protected override void OnDisable()
    {
        Time.timeScale = 1;
        audioM.PlayOrPauseMusic(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public override string Name
    {
        get
        {
            return this.uiId.ToString();
        }
    }

    public override void HandEvent(string eventName, object data)
    {
        
    }
    private void OnResetGame()
    {
        if (GameData.leveName == "level1Enemy")
        {
            audioM.PlayMusic(2);
            UIManager.Instance.HideAllUI(true, null);
            GameSceneManager.Instance.LoadNextSceneAsyn("level01", delegate
            {

                UIManager.Instance.ShowUI(E_UiId.InforUI);

            });
        }
        else if (GameData.leveName == "level2Enemy")
        {
            GameData.oneTirgger = false;
            GameData.towTirrgger = false;
            audioM.PlayMusic(4);

            UIManager.Instance.HideAllUI(true, null);
            GameSceneManager.Instance.LoadNextSceneAsyn("Level02", delegate
            {



                UIManager.Instance.ShowUI(E_UiId.InforUI);
            });
        }
        else if (GameData.leveName == "level3Enemy")
        {
            try
            {
                UIManager.Instance.HideAllUI(true,null);


            }
            catch (System.Exception)
            {

                throw;
            }
            audioM.PlayMusic(6);

            UIManager.Instance.HideSingleUI(E_UiId.StopUI);
            GameSceneManager.Instance.LoadNextSceneAsyn("Level03", delegate
            {
              
                UIManager.Instance.ShowUI(E_UiId.InforUI);
            });
        }
        else if (GameData.leveName == "level4Enemy")
        {
            try
            {
                UIManager.Instance.HideAllUI(true, null);


            }
            catch (System.Exception)
            {

                throw;
            }
            audioM.PlayMusic(2);

            UIManager.Instance.HideSingleUI(E_UiId.StopUI);
            GameSceneManager.Instance.LoadNextSceneAsyn("Level04", delegate
            {

                UIManager.Instance.ShowUI(E_UiId.InforUI);
            });
        }
    }
    private void OnReturnMain()
    {
        audioM.PlayMusic(0);
            UIManager.Instance.HideSingleUI(E_UiId.StopUI);
        GameSceneManager.Instance.LoadNextSceneAsyn("StartSence", delegate
        {
            UIManager.Instance.HideSingleUI(E_UiId.InforUI);
            UIManager.Instance.ShowUI(E_UiId.MainUI);

        });
    }
    private void OnBreakGame()
    {
        UIManager.Instance.HideSingleUI(E_UiId.StopUI);
    }
}

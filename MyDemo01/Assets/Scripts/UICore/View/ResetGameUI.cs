using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGameUI : View
{
    private Button btn_RestGame;
    private Button btn_ReturnMain;
    private Image img_Lock;
    private Image img_Die;
    private Image Img_Win;
    private AudioManager audioM;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        audioM = GameObject.Find("UnitySingletonObj").GetComponent<AudioManager>();
        img_Lock = GameObject.Find("Img_Lock").GetComponent<Image>();
        img_Die = GameTool.GetTheChildComponent<Image>(this.gameObject, "Img_Die");
        Img_Win = GameTool.GetTheChildComponent<Image>(this.gameObject, "Img_Win");
        btn_RestGame = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_RestGame");
        btn_ReturnMain = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_ReturnMain");
        btn_RestGame.onClick.AddListener(OnResetGame);
        btn_ReturnMain.onClick.AddListener(OnReturnMain);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.ResetGameUI;
        uiType.showMode = E_ShowUIMode.HideAll;
    }
    public override string Name
    {
        get
        {
            return this.uiId.ToString();
        }
    }
    protected override void OnEnable()
    {
        img_Lock.enabled = false;
        if (GameData.Win)
        {
            img_Die.enabled = false;
            Img_Win.enabled = true;
        }
        else
        {
            img_Die.enabled = true;
            Img_Win.enabled = false;
        }
        audioM.PlayMusic(1);
        GameData.canLoopMusic = false;
        Cursor.visible = true;
    }
    protected override void OnDisable()
    {
        Cursor.visible = false;
        GameData.canLoopMusic = true;

    }
    public override void HandEvent(string eventName, object data)
    {
    }
    private void OnResetGame()
    {
        if (GameData.leveName == "level1Enemy")
        {
            audioM.PlayMusic(2);
            GameSceneManager.Instance.LoadNextSceneAsyn("level01", delegate
            {

                UIManager.Instance.HideSingleUI(E_UiId.ResetGameUI);
                UIManager.Instance.ShowUI(E_UiId.InforUI);
                
            });
        }
        else if (GameData.leveName == "level2Enemy")
        {
            GameData.oneTirgger = false;
            GameData.towTirrgger = false;
            audioM.PlayMusic(4);

            GameSceneManager.Instance.LoadNextSceneAsyn("Level02", delegate
            {


                UIManager.Instance.HideSingleUI(E_UiId.ResetGameUI);

                UIManager.Instance.ShowUI(E_UiId.InforUI);
            });
        }
        else if (GameData.leveName == "level3Enemy")
        {
            try
            {
            UIManager.Instance.HideSingleUI(E_UiId.EnemyInforUI);

            }
            catch (System.Exception)
            {

                throw;
            }
            audioM.PlayMusic(6);

            GameSceneManager.Instance.LoadNextSceneAsyn("Level03", delegate
            {
                UIManager.Instance.HideSingleUI(E_UiId.ResetGameUI);
                img_Die.enabled = false;
                Img_Win.enabled = true;
                UIManager.Instance.ShowUI(E_UiId.InforUI);
            });
        }
        else if (GameData.leveName == "level4Enemy")
        {
            try
            {
                UIManager.Instance.HideSingleUI(E_UiId.EnemyInforUI);
            }
            catch (System.Exception)
            {

                throw;
            }
            audioM.PlayMusic(6);
            GameSceneManager.Instance.LoadNextSceneAsyn("Level04", delegate
            {
                UIManager.Instance.HideSingleUI(E_UiId.ResetGameUI);
                img_Die.enabled = false;
                Img_Win.enabled = true;
                UIManager.Instance.ShowUI(E_UiId.InforUI);
                if (GameData.restPlayer)
                {
                    GameObject.Find("PlayerHandle").transform.position = new Vector3(12.9f, 2.4f, -17.69f);
                }
            });
        }
    }
    private void OnReturnMain()
    {
        audioM.PlayMusic(0);
        GameSceneManager.Instance.LoadNextSceneAsyn("StartSence", delegate
        {
            UIManager.Instance.HideSingleUI(E_UiId.InforUI);
            UIManager.Instance.ShowUI(E_UiId.MainUI);

        });
    }
}

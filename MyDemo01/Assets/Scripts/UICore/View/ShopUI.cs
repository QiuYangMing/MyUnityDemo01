using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopUI : View
{
    private Button btn_Return;
    private Toggle tog_Equitment;
    private Toggle tog_Potion;

    private Image img_Skill1;
    private Button btn_Buy1;
    private Toggle tog_YES1;
    private Text txt_RedORB1;

    private Image img_Skill2;
    private Button btn_Buy2;
    private Toggle tog_YES2;
    private Text txt_RedORB2;

    private Image img_Skill3;
    private Button btn_Buy3;
    private Toggle tog_YES3;
    private Text txt_RedORB3;

    private Image img_BlueORB;
    private Button btn_Buy4;
    private Text txt_RedORB4;

    private Image img_GreenORB;
    private Button btn_Buy5;
    private Text txt_RedORB5;

    private Image img_Lose;
    private Button btn_Cancel;

    private AudioManager audioM;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        audioM = GameObject.Find("UnitySingletonObj").GetComponent<AudioManager>();
        btn_Return = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Return");
        tog_Equitment = GameTool.GetTheChildComponent<Toggle>(gameObject, "Tog_Equitment");
        tog_Potion = GameTool.GetTheChildComponent<Toggle>(gameObject, "Tog_Potion");
        btn_Return.onClick.AddListener(Close);

        img_Skill1 = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Skill1");
        btn_Buy1 = GameTool.GetTheChildComponent<Button>(img_Skill1.gameObject, "Btn_Buy1");
        tog_YES1 = GameTool.GetTheChildComponent<Toggle>(img_Skill1.gameObject, "Tog_YES1");
        txt_RedORB1 = GameTool.GetTheChildComponent<Text>(img_Skill1.gameObject, "Txt_RedORB1");
        btn_Buy1.onClick.AddListener(
            delegate()
            {
                SellGoods(btn_Buy1, int.Parse(txt_RedORB1.text));
            }
        );
        if (GameData.canMagic)
        {
            UpdateSellState(btn_Buy1, tog_YES1);
        }
        img_Skill2 = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Skill2");
        btn_Buy2 = GameTool.GetTheChildComponent<Button>(img_Skill2.gameObject, "Btn_Buy2");
        tog_YES2 = GameTool.GetTheChildComponent<Toggle>(img_Skill2.gameObject, "Tog_YES2");
        txt_RedORB2 = GameTool.GetTheChildComponent<Text>(img_Skill2.gameObject, "Txt_RedORB2");
        btn_Buy2.onClick.AddListener(
            delegate ()
            {
                SellGoods(btn_Buy2, int.Parse(txt_RedORB2.text));
            }
        );
        if (GameData.canFireAttack)
        {
            UpdateSellState(btn_Buy2, tog_YES2);
        }
        img_Skill3 = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Skill3");
        btn_Buy3 = GameTool.GetTheChildComponent<Button>(img_Skill3.gameObject, "Btn_Buy3");
        tog_YES3 = GameTool.GetTheChildComponent<Toggle>(img_Skill3.gameObject, "Tog_YES3");
        txt_RedORB3 = GameTool.GetTheChildComponent<Text>(img_Skill3.gameObject, "Txt_RedORB3");
        btn_Buy3.onClick.AddListener(
            delegate ()
            {
                SellGoods(btn_Buy3, int.Parse(txt_RedORB3.text));
            }
        );
        if (GameData.canConterBack)
        {
            UpdateSellState(btn_Buy3, tog_YES3);
        }
        img_BlueORB = GameTool.GetTheChildComponent<Image>(gameObject, "Img_BlueORB");
        btn_Buy4 = GameTool.GetTheChildComponent<Button>(img_BlueORB.gameObject, "Btn_Buy4");
        txt_RedORB4 = GameTool.GetTheChildComponent<Text>(img_BlueORB.gameObject, "Txt_RedORB4");
        btn_Buy4.onClick.AddListener(
            delegate ()
            {
                SellGoods(btn_Buy4, int.Parse(txt_RedORB4.text));
            }
        );
        img_GreenORB = GameTool.GetTheChildComponent<Image>(gameObject, "Img_GreenORB");
        btn_Buy5 = GameTool.GetTheChildComponent<Button>(img_GreenORB.gameObject, "Btn_Buy5");
        txt_RedORB5 = GameTool.GetTheChildComponent<Text>(img_GreenORB.gameObject, "Txt_RedORB5");
        btn_Buy5.onClick.AddListener(
            delegate ()
            {
                SellGoods(btn_Buy5, int.Parse(txt_RedORB5.text));
            }
        );
        img_Lose = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Lose");
        btn_Cancel = GameTool.GetTheChildComponent<Button>(img_Lose.gameObject, "Btn_Cancel");
        btn_Cancel.onClick.AddListener(Cancel);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.ShopUI;
        if (GameData.canConterBack)
        {
            btn_Buy3.enabled = false;
            tog_YES3.enabled = true;
            tog_YES3.isOn = true;
        }
        if (GameData.canMagic)
        {
            btn_Buy1.enabled = false;
            tog_YES1.enabled = true;
            tog_YES1.isOn = true;
        }
        if (GameData.canFireAttack)
        {
            btn_Buy2.enabled = false;
            tog_YES2.enabled = true;
            tog_YES2.isOn = true;
        }

    }
    protected override void OnEnable()
    {
      
        audioM.PlayMusic(8);
        Cursor.visible = true;
        
    }
    protected override void OnDisable()
    {
        switch (GameData.leveName)
        {
            case "level1Enemy":
                audioM.PlayMusic(2);
                break;
            case "level2Enemy":
                audioM.PlayMusic(4);
                break;
            default:
                audioM.PlayMusic(2);
                break;
        }
        Cursor.visible = false;


    }
    public override string Name
    {
        get
        {
            return uiId.ToString();
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(GameDefine.message_UpdateSkill1);
        AttentionEvents.Add(GameDefine.message_UpdateSkill2);
        AttentionEvents.Add(GameDefine.message_UpdateSkill3);
    }
    public override void HandEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case GameDefine.message_UpdateSkill1:
                UpdateSellState(btn_Buy1,tog_YES1);
                break;
            case GameDefine.message_UpdateSkill2:
                UpdateSellState(btn_Buy2, tog_YES2);

                break;
            case GameDefine.message_UpdateSkill3:
                UpdateSellState(btn_Buy3, tog_YES3);

                break;
          
            default:
                break;
        }
    }
    protected override void Update()
    {
        if (tog_Equitment.isOn)
        {
            img_Skill1.gameObject.SetActive(true);
            img_Skill2.gameObject.SetActive(true);
            img_Skill3.gameObject.SetActive(true);
            img_BlueORB.gameObject.SetActive(false);
            img_GreenORB.gameObject.SetActive(false);
        }
        if (tog_Potion.isOn)
        {
            img_Skill1.gameObject.SetActive(false);
            img_Skill2.gameObject.SetActive(false);
            img_Skill3.gameObject.SetActive(false);
            img_BlueORB.gameObject.SetActive(true);
            img_GreenORB.gameObject.SetActive(true);
        }
        if (!tog_Equitment.isOn && !tog_Potion.isOn)
        {
            img_Skill1.gameObject.SetActive(false);
            img_Skill2.gameObject.SetActive(false);
            img_Skill3.gameObject.SetActive(false);
            img_BlueORB.gameObject.SetActive(false);
            img_GreenORB.gameObject.SetActive(false);
        }

        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void UpdateSellState(Button btn,Toggle tog)
    {
        btn.gameObject.SetActive(false);
        tog.gameObject.SetActive(true);
        tog.isOn = true;
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SellGoods(Button btn,int redORB)
    {
        if (GetModel<InforData>().GetRedORB() >= redORB)
        {
            switch (btn.name)
            {
                case "Btn_Buy1":
                    
                    GetModel<ShopData>().EditorMagic(1);
                    break;
                case "Btn_Buy2":
                    GetModel<InforData>().EditorRedORB((GetModel<InforData>().GetRedORB() - redORB));
                    GetModel<ShopData>().EditorFireAttack(1);
                    if (GameData.firstBuyFireAttack)
                    {
                        MVC.SendEvent(GameDefine.command_UpdateTips, "BO13");
                        GameTool.SetInt("firstBuyFireAttack", 0);
                        GameData.firstBuyFireAttack = false;
                    }
                    break;
                case "Btn_Buy3":
                    GetModel<InforData>().EditorRedORB((GetModel<InforData>().GetRedORB() - redORB));
                    GetModel<ShopData>().EditorConterBack(1);
                    if (GameData.firstBuyConterBack)
                    {
                        MVC.SendEvent(GameDefine.command_UpdateTips, "BO16");
                        GameTool.SetInt("firstBuyConterBack", 0);
                        GameData.firstBuyConterBack = false;
                    }
                    break;
                case "Btn_Buy4":
                    GetModel<InforData>().EditorRedORB((GetModel<InforData>().GetRedORB() - redORB));
                    GetModel<InforData>().EditorBlueORB(GetModel<InforData>().GetBlueORB() + 1);
                    break;
                case "Btn_Buy5":
                    GetModel<InforData>().EditorRedORB((GetModel<InforData>().GetRedORB() - redORB));
                    GetModel<InforData>().EditorGreenORB(GetModel<InforData>().GetGreenORB() + 1);
                    break;
                default:
                    break;
            }
        }
        else 
        {
            img_Lose.gameObject.SetActive(true);
        }
        
    }
    private void Cancel()
    {
        img_Lose.gameObject.SetActive(false);
    }
}

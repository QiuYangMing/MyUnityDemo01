using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InforUI : View
{
    private Image img_Effect;
    private Image img_Point;
    private Text txt_RedORB;
    private Text txt_GreenORB;
    private Text txt_BlueORB;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        img_Effect = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Effect");
        img_Point = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Point");
        txt_RedORB = GameTool.GetTheChildComponent<Text>(gameObject, "Txt_RedORB");
        txt_GreenORB = GameTool.GetTheChildComponent<Text>(gameObject, "Txt_GreenORB");
        txt_BlueORB = GameTool.GetTheChildComponent<Text>(gameObject, "Txt_BlueORB");


    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.InforUI;
        uiType.uiRootType = E_UIRootType.KeepAbove;
        txt_BlueORB.text = GetModel<InforData>().GetBlueORB().ToString();
        txt_RedORB.text = GetModel<InforData>().GetRedORB().ToString();
        txt_GreenORB.text = GetModel<InforData>().GetGreenORB().ToString();

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
        AttentionEvents.Add(GameDefine.message_UpdatePoint);
        AttentionEvents.Add(GameDefine.message_UpdateRedORB);
        AttentionEvents.Add(GameDefine.message_UpdateGreenORB);
        AttentionEvents.Add(GameDefine.message_UpdateBlueORB);

    }
    protected override void OnEnable()
    {
        GameData.hp = GameData.maxHp;
        StartCoroutine(UpdateHpCo(GameData.hp / GameData.maxHp));

    }
    public override void HandEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case GameDefine.message_UpdatePoint:
                StartCoroutine(UpdateHpCo((float)data));
                break;
            case GameDefine.message_UpdateRedORB:
                UpdateRedORB((int)data);
                break;
            case GameDefine.message_UpdateGreenORB:
                UpdateGreenORB((int)data);
                break;
            case GameDefine.message_UpdateBlueORB:
                UpdateBlueORB((int)data);
                break;
            default:
                break;
        }
    }
    
    private void UpdateRedORB(int num)
    {
        txt_RedORB.text = num.ToString();
    }
    private void UpdateGreenORB(int num)
    {
        txt_GreenORB.text = num.ToString();
    }
    private void UpdateBlueORB(int num)
    {
        txt_BlueORB.text = num.ToString();
    }
    IEnumerator UpdateHpCo(float percent)
    {
        img_Point.fillAmount = percent;
        while (img_Effect.fillAmount > img_Point.fillAmount)
        {
            img_Effect.fillAmount -= 0.03f;
            yield return new WaitForSeconds(0.1f);
        }
        if (img_Effect.fillAmount < img_Point.fillAmount)
        {
            img_Effect.fillAmount = img_Point.fillAmount;
        }
    }
}

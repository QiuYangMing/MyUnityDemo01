using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInforUI : View
{
    private Image img_Effect;
    private Image img_Point;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        img_Effect = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Effect");
        img_Point = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Point");
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.EnemyInforUI;
        uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    protected override void OnEnable()
    {
        GameData.EnHp = GameData.EnMaxHp;
        StartCoroutine(UpdateHpCo(GameData.EnHp / GameData.EnMaxHp));
    }
    public override string Name
    {
        get
        {
           return this.uiId.ToString();
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(GameDefine.message_EnUpdatePoint);
    }
    public override void HandEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case GameDefine.message_EnUpdatePoint:
                StartCoroutine(UpdateHpCo((float)data));
                break;
            
            default:
                break;
        }
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

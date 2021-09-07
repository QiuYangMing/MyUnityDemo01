using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : View
{
    private Slider slider_Progress;
    private Text txt_Porgress;
    private Sprite[] hints ;
    private Image backgrand;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        backgrand = GetComponent<Image>();
        slider_Progress = GameTool.GetTheChildComponent<Slider>(this.gameObject, "Slider_Progress");
        slider_Progress.onValueChanged.AddListener(UpdateProgress);
        txt_Porgress = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Porgress");
        hints = Resources.LoadAll<Sprite>("Hint");
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.LoadingUI;
    }
    protected override void OnEnable()
    {
        if (GameData.leveName == "level1Enemy")
        {
            backgrand.sprite = hints[0];
        }
        else if (GameData.leveName == "level2Enemy")
        {
            backgrand.sprite = hints[1];
        }
        else if (GameData.leveName == "level3Enemy")
        {
            backgrand.sprite = hints[2];
        }
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
    private void UpdateProgress(float value)
    {
        txt_Porgress.text = Mathf.RoundToInt(value * 100).ToString() + "%";
    }
}

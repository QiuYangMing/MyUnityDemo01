using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsUI : View
{
    private Image img_Reveal;
    private Image img_Describe;
    private Image img_Explain;
    private Button btn_Close;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        img_Reveal = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Reveal");
        img_Describe = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Describe");
        img_Explain = GameTool.GetTheChildComponent<Image>(gameObject, "Img_Explain");
        btn_Close = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Close");
        btn_Close.onClick.AddListener(DisableUI);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.TipsUI;
        uiType.uiRootType = E_UIRootType.KeepAbove;
    }

    protected override void OnEnable()
    {
        Time.timeScale = 0.1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    protected override void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    protected override void Update()
    {
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
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
        AttentionEvents.Add(GameDefine.message_UpdateTips);
    }
    public override void HandEvent(string eventName, object data)
    {

        img_Reveal.sprite = ((Sprite[])data)[0];
        img_Describe.sprite = ((Sprite[])data)[1];
        img_Explain.sprite = ((Sprite[])data)[2];
    }

    private void DisableUI()
    {
        UIManager.Instance.HideSingleUI(E_UiId.TipsUI);
    }
}

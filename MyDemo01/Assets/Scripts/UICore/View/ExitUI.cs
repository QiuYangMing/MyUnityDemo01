using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitUI : View
{
    private Button btn_Cancel;
    private Button btn_Exit;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Cancel = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Cancel");
        btn_Exit = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Exit");
        btn_Exit.onClick.AddListener(ExitGame);
        btn_Cancel.onClick.AddListener(Close);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.ExitUI;
        uiType.showMode = E_ShowUIMode.DoNothing;
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
    private void ExitGame()
    {
        Application.Quit();
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);
    }
}

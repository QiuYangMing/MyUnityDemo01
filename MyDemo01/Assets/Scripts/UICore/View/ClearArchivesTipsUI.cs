using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearArchivesTipsUI : View
{

    private Button btn_Exit;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Exit = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Exit");
        btn_Exit.onClick.AddListener(HideUI);
    }

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        uiId = E_UiId.ClearArchivesTipsUI;
        uiType.uiRootType = E_UIRootType.KeepAbove;
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
        throw new System.NotImplementedException();
    }

    private void HideUI()
    {
        UIManager.Instance.HideSingleUI(E_UiId.ClearArchivesTipsUI);
    }
}

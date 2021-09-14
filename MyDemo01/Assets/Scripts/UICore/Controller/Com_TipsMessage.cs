using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_TipsMessage : Controller
{
    public override void Execute(object data)
    {
        UIManager.Instance.ShowUI(E_UiId.TipsUI);
        GetModel<TipsMessageData>().EditorTipsMessage((string)data);
    }
}

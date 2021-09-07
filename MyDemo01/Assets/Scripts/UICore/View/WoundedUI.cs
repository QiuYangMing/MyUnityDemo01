using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundedUI : View
{
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.WoundedUI;
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
}

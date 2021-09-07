using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_EnUpdataHp : Controller
{
    public override void Execute(object data)
    {
       
        
            GetModel<EnInforData>().EditorHP((float)data);

        
    }
}

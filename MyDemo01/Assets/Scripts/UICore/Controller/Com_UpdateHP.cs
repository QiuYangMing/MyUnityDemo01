using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_UpdateHP : Controller
{
    public override void Execute(object data)
    {
        if ((float)data >0 && GetModel<InforData>().GetGreenORB() > 0)
        {
            GetModel<InforData>().EditorGreenORB(GetModel<InforData>().GetGreenORB() - 1);
            GetModel<InforData>().EditorHP((float)data);
            return;
        }
        else if ((float)data < 0)
        {
             GetModel<InforData>().EditorHP((float)data);

        }
        
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_UpdateProps : Controller
{
    public override void Execute(object data)
    {
        //传入一个列表，第一个为道具类型，第二个为道具数量
        if (((List<string>)data)[0] == "Blue")
        {
            GetModel<InforData>().EditorBlueORB(GetModel<InforData>().GetBlueORB() + int.Parse(((List<string>)data)[1] ));
        }
        else if (((List<string>)data)[0] == "Green")
        {
            GetModel<InforData>().EditorGreenORB(GetModel<InforData>().GetGreenORB() + int.Parse(((List<string>)data)[1]));
        }
    }
}

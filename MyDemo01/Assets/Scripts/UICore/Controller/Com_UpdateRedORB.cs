using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_UpdateRedORB : Controller
{
    public override void Execute(object data)
    {
        //获取视图层
        InforUI view = GetView<InforUI>();
        //获取红魔石数量
        int currentRedORB = GetModel<InforData>().GetRedORB();
        int newRedORBCount = (int)data + currentRedORB;
        GetModel<InforData>().EditorRedORB(newRedORBCount);
    }
}

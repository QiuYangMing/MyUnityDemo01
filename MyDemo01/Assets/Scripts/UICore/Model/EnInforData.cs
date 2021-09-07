using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnInforData : Model
{
    public override string Name
    {
        get
        {
            return "EnInforData";
        }
    }
    public void EditorHP(float newHP)
    {
        GameData.EnHp += newHP;
        GameData.EnHp = Mathf.Clamp(GameData.EnHp, 0, GameData.EnMaxHp);
        SendEvent(GameDefine.message_EnUpdatePoint, GameData.EnHp / GameData.EnMaxHp);
    }
}

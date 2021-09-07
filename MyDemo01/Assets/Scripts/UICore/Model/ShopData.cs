using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : Model
{
    public override string Name
    {
        get
        {
            return "ShopData";
        }
    }
    public void InitShopData()
    {
        if (!GameTool.HasKey("Magic"))
        {
            GameTool.SetInt("Magic", 0);
        }
        GameData.canMagic = GameTool.GetInt("Magic") == 0 ? false : true;
        if (!GameTool.HasKey("fireAttack"))
        {
            GameTool.SetInt("fireAttack", 0);
        }
        GameData.canFireAttack = GameTool.GetInt("fireAttack") == 0 ? false : true;
        if (!GameTool.HasKey("conterBack"))
        {
            GameTool.SetInt("conterBack", 0);
        }
        GameData.canConterBack = GameTool.GetInt("conterBack") == 0 ? false : true;
    }
    public void EditorMagic(int newState)
    {
        GameTool.SetInt("Magic", newState);
        GameData.canMagic = newState == 0 ? false : true;
        SendEvent(GameDefine.message_UpdateSkill1);
    }
    public void EditorFireAttack(int newState)
    {
        GameTool.SetInt("fireAttack", newState);
        GameData.canFireAttack = newState == 0 ? false : true;
        SendEvent(GameDefine.message_UpdateSkill2);
    }
    public void EditorConterBack(int newState)
    {
        GameTool.SetInt("conterBack", newState);
        GameData.canConterBack = newState == 0 ? false : true;
        SendEvent(GameDefine.message_UpdateSkill3);
    }
}

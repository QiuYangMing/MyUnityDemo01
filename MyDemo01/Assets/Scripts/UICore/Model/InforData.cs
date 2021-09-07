using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InforData : Model
{
   
    int greenORB;
    int redORB;
    int blueORB;
    public override string Name
    {
        get
        {
            return "InforData";
        }
        
    }
    public void InitInforData()
    {
        //GameTool.DeleteAll();
        if (!GameTool.HasKey("BlueORB"))
        {
            GameTool.SetInt("BlueORB", 0);
        }
        blueORB = GameTool.GetInt("BlueORB");
        if (!GameTool.HasKey("MaxHp"))
        {
            GameTool.SetFloat("MaxHp", 100 + blueORB * 20);
        }
        GameData.maxHp = GameTool.GetFloat("MaxHp");
        GameData.hp = GameData.maxHp;
        if (!GameTool.HasKey("RedORB"))
        {
            GameTool.SetInt("RedORB", 0);
        }
        redORB = GameTool.GetInt("RedORB");
        if (!GameTool.HasKey("GreenORB"))
        {
            GameTool.SetInt("GreenORB", 0);
        }
        greenORB = GameTool.GetInt("GreenORB");

        if (!GameTool.HasKey("Leve1Enter"))
        {
            GameTool.SetInt("Leve1Enter", 0);
        }
        GameData.leve1Enter = GameTool.GetInt("Leve1Enter");
        if (!GameTool.HasKey("Leve2Enter"))
        {
            GameTool.SetInt("Leve2Enter", 0);
        }
        GameData.leve2Enter = GameTool.GetInt("Leve2Enter");
        if (!GameTool.HasKey("Leve3Enter"))
        {
            GameTool.SetInt("Leve3Enter", 0);
        }
        GameData.leve3Enter = GameTool.GetInt("Leve3Enter");

    }
    //对外提供，获取数据
    
    public int GetRedORB()
    {
        return redORB;
    }
    public int GetBlueORB()
    {
        return blueORB;
    }
    public int GetGreenORB()
    {
        return greenORB;
    }
    //对外提供，修改数据
    public void EditorMaxHp(float newMaxHp)
    {
        GameTool.SetFloat("MaxHp", newMaxHp);
        GameData.maxHp = newMaxHp;
        GameData.hp = newMaxHp;
        SendEvent(GameDefine.message_UpdatePoint, GameData.hp / GameData.maxHp);
    }
    public void EditorHP(float newHP)
    {
        GameData.hp += newHP;
        GameData.hp = Mathf.Clamp(GameData.hp, 0, GameData.maxHp);
        SendEvent(GameDefine.message_UpdatePoint, GameData.hp / GameData.maxHp);
    }
    public void EditorRedORB(int newRedORBCount)
    {
        GameTool.SetInt("RedORB", newRedORBCount);
        redORB = newRedORBCount;
        SendEvent(GameDefine.message_UpdateRedORB, redORB);
    }
    public void EditorGreenORB(int newGreenORBCount)
    {
        GameTool.SetInt("GreenORB", newGreenORBCount);
        greenORB = newGreenORBCount;
        SendEvent(GameDefine.message_UpdateGreenORB, greenORB);
    }
    public void EditorBlueORB(int newBlueORBCount)
    {
        GameTool.SetInt("BlueORB", newBlueORBCount);
        blueORB = newBlueORBCount;
        SendEvent(GameDefine.message_UpdateBlueORB, blueORB);
        EditorMaxHp(GameTool.GetFloat("MaxHp") + GameTool.GetInt("BlueORB") * 20);
    }
}

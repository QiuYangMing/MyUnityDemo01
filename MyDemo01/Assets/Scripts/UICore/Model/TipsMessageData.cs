using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsMessageData : Model
{

    private Sprite[] tipsMessages;
    public override string Name
    {
        get
        {
            return "TipsMessageData";
        }
    }

    public void InitTipsMessages()
    {
        tipsMessages = Resources.LoadAll<Sprite>("TipsMessages");
        if (!GameTool.HasKey("firstInBaseOperation"))
        {
            GameTool.SetInt("firstInBaseOperation", 1);
        }
        GameData.firstInBaseOperation = GameTool.GetInt("firstInBaseOperation") == 1 ? true : false;
        if (!GameTool.HasKey("firstInStopUIOperation"))
        {
            GameTool.SetInt("firstInStopUIOperation", 1);
        }
        GameData.firstInStopUIOperation = GameTool.GetInt("firstInStopUIOperation") == 1 ? true : false;
        if (!GameTool.HasKey("firstInBattleOperation"))
        {
            GameTool.SetInt("firstInBattleOperation", 1);
        }
        GameData.firstInBattleOperation = GameTool.GetInt("firstInBattleOperation") == 1 ? true : false;
        if (!GameTool.HasKey("firstInMagicEnter"))
        {
            GameTool.SetInt("firstInMagicEnter", 1);
        }
        GameData.firstInMagicEnter = GameTool.GetInt("firstInMagicEnter") == 1 ? true : false;
        if (!GameTool.HasKey("firstBuyFireAttack"))
        {
            GameTool.SetInt("firstBuyFireAttack", 1);
        }
        GameData.firstBuyFireAttack = GameTool.GetInt("firstBuyFireAttack") == 1 ? true : false;
        if (!GameTool.HasKey("firstBuyConterBack"))
        {
            GameTool.SetInt("firstBuyConterBack", 1);
        }
        GameData.firstBuyConterBack = GameTool.GetInt("firstBuyConterBack") == 1 ? true : false;
        if (!GameTool.HasKey("firstGetBlueORB"))
        {
            GameTool.SetInt("firstGetBlueORB", 1);
        }
        GameData.firstGetBlueORB = GameTool.GetInt("firstGetBlueORB") == 1 ? true : false;
        if (!GameTool.HasKey("firstGetGreenORB"))
        {
            GameTool.SetInt("firstGetGreenORB", 1);
        }
        GameData.firstGetGreenORB = GameTool.GetInt("firstGetGreenORB") == 1 ? true : false;
        if (!GameTool.HasKey("firstGetRedORB"))
        {
            GameTool.SetInt("firstGetRedORB", 1);
        }
        GameData.firstGetRedORB = GameTool.GetInt("firstGetRedORB") == 1 ? true : false;
    }

    //对外提供,修改Tips里的数据
    public void EditorTipsMessage(string nodeName)
    {
        for (int i = 0; i < tipsMessages.Length; i++)
        {
            if (nodeName == tipsMessages[i].name)
            {
                Sprite[] tips_param ={tipsMessages[i],tipsMessages[i+1],tipsMessages[i+2] };
                SendEvent(GameDefine.message_UpdateTips, tips_param);
                break;
            }
        }
    }
}

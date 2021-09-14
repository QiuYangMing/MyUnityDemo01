using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (this.gameObject.name)
            {
                case "Tips01":
                    if (GameData.firstInStopUIOperation)
                    {
                        MVC.SendEvent(GameDefine.command_UpdateTips, "BO04");
                        GameTool.SetInt("firstInStopUIOperation", 0);
                        GameData.firstInStopUIOperation = false;
                    }
                    break;
                case "Tips02":
                    if (GameData.firstInBattleOperation)
                    {
                        MVC.SendEvent(GameDefine.command_UpdateTips, "BO07");
                        GameTool.SetInt("firstInBattleOperation", 0);
                        GameData.firstInBattleOperation = false;
                    }
                    break;
                case "Tips03":
                    if (GameData.firstInMagicEnter)
                    {
                        MVC.SendEvent(GameDefine.command_UpdateTips, "BO10");
                        GameTool.SetInt("firstInMagicEnter", 0);
                        GameData.firstInMagicEnter = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

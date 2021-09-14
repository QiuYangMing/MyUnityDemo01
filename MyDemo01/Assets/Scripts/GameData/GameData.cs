using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public static float hp;
    public static float maxHp;
    public static float EnHp;
    public static float EnMaxHp;
    public static float airHigh = 2.5f;
    public static bool canConterBack;
    public static bool canFireAttack;
    public static bool canMagic;
    public static bool firestHit =true;
    public static int enemyNum = -1;
    public static int SphereL = 0;
    public static int SphereR = 0;
    public static int leve1Enter;
    public static int leve2Enter;
    public static int leve3Enter;
    public static string leveName;
    public static bool oneTirgger = false;
    public static bool towTirrgger = false;
    public static bool Win = false;
    public static bool restPlayer = false;
    public static bool canLoopMusic = true;
    //第一次进入游戏(新手教程)
    public static bool firstInBaseOperation = true;
    public static bool firstInStopUIOperation = true;
    public static bool firstInBattleOperation = true;
    public static bool firstInMagicEnter = true;
    public static bool firstBuyFireAttack = true;
    public static bool firstBuyConterBack = true;
    public static bool firstGetBlueORB = true;
    public static bool firstGetGreenORB = true;
    public static bool firstGetRedORB = true;
}

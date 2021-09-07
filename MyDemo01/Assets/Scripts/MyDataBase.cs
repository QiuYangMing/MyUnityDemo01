using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataBase
{

    private string enemyDataBaseFileName;
    public readonly JSONObject enemtDataBase;
    public MyDataBase(string _enemyDataBaseFileName)
    {
        enemyDataBaseFileName = _enemyDataBaseFileName;
        TextAsset WeaponContent = Resources.Load(enemyDataBaseFileName) as TextAsset;
        enemtDataBase = new JSONObject(WeaponContent.text);
        
    }
}

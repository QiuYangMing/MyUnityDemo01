using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatLevel4Enemy : MonoBehaviour
{
    private List<Transform> trans_creatEnemys = new List<Transform>();
    private MyDataBase myDataBase;
    private EnemyFactory enemyFactory;
    void Start()
    {
        InitDataBase();
        InitEnemyFactory();
        foreach (Transform trans in transform)
        {
            trans_creatEnemys.Add(trans);
        }
        StartCoroutine(enemyFactory.CreatEnemy(trans_creatEnemys));
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitDataBase()
    {
        myDataBase = new MyDataBase(GameData.leveName);
    }
    private void InitEnemyFactory()
    {
        enemyFactory = new EnemyFactory(myDataBase);
    }

}

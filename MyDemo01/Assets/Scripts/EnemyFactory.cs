using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private MyDataBase enemyData;
    public EnemyFactory(MyDataBase _enemyData)
    {
        enemyData = _enemyData;
    }
    public IEnumerator  CreatEnemy( List<Transform> trans_enemys)
    {
        GameObject enemy;
        for (int i = 0; i < enemyData.enemtDataBase.list.Count; i++)
        {
            switch (enemyData.enemtDataBase[i]["Name"].str)
            {
                case "EnemyHandle_S_A":
                    enemy = Resources.Load<GameObject>("EnemyHandle_S_A");
                    GameObject EnemyHandle_S_A = GameObject.Instantiate(enemy, trans_enemys[i].position, trans_enemys[i].rotation);
                    EnemyHandle_S_A.GetComponent<EnStateManager>().HP = enemyData.enemtDataBase[i]["HP"].f;
                    EnemyHandle_S_A.GetComponent<EnStateManager>().HPMax = enemyData.enemtDataBase[i]["MaxHP"].f;
                    EnemyHandle_S_A.GetComponent<EnStateManager>().Atk = enemyData.enemtDataBase[i]["ATK"].f;
                    yield return new WaitForSeconds(0f);
                    break;
                case "EnemyHandle_Archer_A":
                    enemy = Resources.Load<GameObject>("EnemyHandle_Archer_A");
                    GameObject EnemyHandle_Archer_A = GameObject.Instantiate(enemy, trans_enemys[i].position, trans_enemys[i].rotation);
                    EnemyHandle_Archer_A.GetComponent<EnStateManager>().HP = enemyData.enemtDataBase[i]["HP"].f;
                    EnemyHandle_Archer_A.GetComponent<EnStateManager>().HPMax = enemyData.enemtDataBase[i]["MaxHP"].f;
                    EnemyHandle_Archer_A.GetComponent<EnStateManager>().Atk = enemyData.enemtDataBase[i]["ATK"].f;
                    yield return new WaitForSeconds(0f);
                    break;
                case "EnemyHandle":
                    enemy = Resources.Load<GameObject>("EnemyHandle");
                    GameObject EnemyHandle = GameObject.Instantiate(enemy, trans_enemys[i].position, trans_enemys[i].rotation);
                    GameData.EnHp =  EnemyHandle.GetComponent<EnStateManager>().HP = enemyData.enemtDataBase[i]["HP"].f;
                    GameData.EnMaxHp =  EnemyHandle.GetComponent<EnStateManager>().HPMax = enemyData.enemtDataBase[i]["MaxHP"].f;
                    EnemyHandle.GetComponent<EnStateManager>().Atk = enemyData.enemtDataBase[i]["ATK"].f;
                    //UIManager.Instance.ShowUI(E_UiId.EnemyInforUI);
                    yield return new WaitForSeconds(0f);
                    break;
                default:
                    break;
            }
        }
    }
	
}

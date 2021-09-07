using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatEnemy : MonoBehaviour
{

    private List<Transform> trans_CreatEnemys = new List<Transform>();
    private GameObject creatShop;
    private GameObject nextLevel;
    private BoxCollider door;
    private ParticleSystem doorEffect;
    private MyDataBase myDataBase;
    private EnemyFactory enemyFactory;

    private AudioManager audioM;
    void Start () {
        InitmyDataBase();
        InitEnemyFactory();
        audioM = GameObject.Find("Canvas(Clone)").transform.Find("UnitySingletonObj").GetComponent<AudioManager>();
        door = GetComponent<BoxCollider>();
        creatShop = Resources.Load<GameObject>("OpenShop");
        nextLevel = Resources.Load<GameObject>("NextLevel");

        doorEffect = transform.Find("DoorEffect").GetComponent<ParticleSystem>();
        foreach (Transform trans in transform)
        {
            trans_CreatEnemys.Add(trans);
        }
        GameData.enemyNum = trans_CreatEnemys.Count -1;
	}

    private void Update()
    {
        if (GameData.enemyNum == 0)
        {
            if (GameData.leveName != "level3Enemy")
            {
            Instantiate(creatShop, trans_CreatEnemys[3].transform.position - Vector3.up*0.3f,Quaternion.identity);
            Instantiate(nextLevel, trans_CreatEnemys[3].transform.position - Vector3.up * 0.5f-Vector3.right*4f, Quaternion.identity);

            }
            else
            {
                GameData.Win = true;
            }

            GameData.enemyNum = -1;
        }
    }
    private void InitEnemyFactory()
    {
        enemyFactory = new EnemyFactory(myDataBase);
    }
    private void InitmyDataBase()
    {
        myDataBase = new MyDataBase(GameData.leveName);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        switch (GameData.leveName)
        {
            case "level1Enemy":
                audioM.PlayMusic(3);
                break;
            case "level2Enemy":
                audioM.PlayMusic(5);
                break;
            case "level3Enemy":
                audioM.PlayMusic(7);
                break;
            default:
                break;
        }
        door.isTrigger = false;
        doorEffect.Play();
        StartCoroutine(enemyFactory.CreatEnemy(trans_CreatEnemys));
        
    }
   
}

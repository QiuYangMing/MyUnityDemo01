using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    private GameObject RedORB;
    private GameObject RedORB2;
    private void Start()
    {
        RedORB = Resources.Load<GameObject>("RedORB");
        StartCoroutine(CreatRedORB());
    }
    IEnumerator CreatRedORB()
    {
        yield return new WaitForSeconds(1f);
       
            RedORB2 = Instantiate(RedORB, transform.position + Vector3.up, transform.rotation);
        GameData.enemyNum -= 1;

        
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarp : MonoBehaviour {

   
    private void Update()
    {
       
          
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameData.hp > 0 )
        {
            collision.gameObject.GetComponent<ActorManager>().sm.AddHp(-30f);
            if (GameData.hp > 0)
            {
            collision.gameObject.GetComponent<ActorManager>().Hit();

            }
            else
            {
             collision.gameObject.GetComponent<ActorManager>().Die();
            }
        }
        else
        {
            if (collision.gameObject.GetComponent<ActorController>().m_Respawning)
            {
                return;
            }
            collision.gameObject.GetComponent<ActorManager>().Die();
        }
    }
}

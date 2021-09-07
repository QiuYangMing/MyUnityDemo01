using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletion_AAttack : MonoBehaviour {

    private BlackKnightController bc;
    private float dist;
    private MyTime myTime = new MyTime();
    private int attackCount = 0;
    private EnStateManager esm;
    private void Start()
    {
        bc = GetComponent<BlackKnightController>();
        esm = GetComponent<EnStateManager>();
    }
    private void FixedUpdate()
    {
        dist = Vector3.Distance(bc.ei.Player.transform.position, transform.position);
        if (bc.ei.Player != null && !bc.ei.Player.GetComponent<ActorController>().m_Respawning)
        {
            myTime.Tick();
            if (bc.canAttack &&  dist< 2f && !esm.isLock)
            {
                Attack(1);
                attackCount++;
                if (attackCount == 3)
                {
                    bc.canAttack = false;
                    bc.StartTimer(myTime, 1f);
                    attackCount = 0;
                }

            }
            if (myTime.state == MyTime.STATE.FINISHED)
            {
                bc.canAttack = true;
                myTime.RestState();
            }
        }
    }
   
    private void Attack(int x)
    {
        switch (x)
        {
            case 1:
                bc.IssueTrigger("Attack");
                break;
           
           
            default:
                break;
        }
    }
}

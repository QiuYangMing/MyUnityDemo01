using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKningAttack : MonoBehaviour
{
    private BlackKnightController bc;
    private BlackKnightInput bi;
    private Vector3 toPlayer;
    private Vector3 target;
    private bool canMove;
    private float angel;
    private int candge;
    private float dist;
    private void Awake()
    {
        bc = GetComponent<BlackKnightController>();
        bi = GetComponent<BlackKnightInput>();
        
    }
    private void Start()
    {
        bi.lockon = true;
        canMove = true;
    }
    private void FixedUpdate()
    {
        dist = Vector3.Distance(transform.position, bi.Player.transform.position);
        toPlayer = bi.Player.transform.position - transform.position;
        angel = Vector3.Angle(transform.forward, toPlayer);
        if (bi.Player != null && !bi.Player.GetComponent<ActorController>().m_Respawning)
        {
            if (dist <= 2f && angel <= 60f)
            {
                if (Random.Range(-1, 1f) >= 0)
                {
                    bc.IssueTrigger("Attack");
                }
                else
                {
                    bc.IssueTrigger("attack4");

                }
            }
            else if (dist <= 2f && angel > 84f)
            {
                bc.IssueTrigger("BackAttack");
            }
            else if (candge >= 3 && dist >= 2f && dist < 5f)
            {
                bc.IssueTrigger("Dodge");
            }
            else if ( dist > 2f && dist <= 5f)
            {
                bc.IssueTrigger("sp_attack");
            }
            else if (dist > 5f && dist < bi.attackRange)
            {
                bc.IssueTrigger("Arrow");
            }
            else if (dist > bi.attackRange)
            {
                bi.navMeshAgent.speed = 5f;
            }
        }
    }
    private void OnAttack2Enter()
    {
        candge += 2;
        if (dist <= 1.5f)
        {
            bc.transform.LookAt(bi.Player.transform);
            bc.IssueTrigger("Attack");
            candge++;
        }
        else if (dist > 1.5f && dist <= 2.0f)
        {
            bc.transform.LookAt(bi.Player.transform);
            bc.IssueTrigger("Attack2");
            candge++;

        }
        else if (dist > 2.0f && dist <= 3.0f)
        {
            bc.transform.LookAt(bi.Player.transform);
            bc.IssueTrigger("Attack3");
            candge++;

        }
    }

    private void OnDodgeEnter()
    {
        candge = 0;
        bc.rigid.AddForce(transform.forward*500f);
        bc.IssueTrigger("Attack");
    }
    private void OnShieldEnter()
    {
        if (Random.Range(-1, 1f) >= 0)
        {
            bc.IssueTrigger("attack4");
        }
        else
        {
            bc.IssueBool("Defense",true);
            StartCoroutine(CloseDefense());
        }
    }
    private void OnAttack3_03Enter()
    {
        StartCoroutine(Attack3_03());
        bc.rigid.AddForce(transform.up * 100f);
       
    }
    
   
    IEnumerator Attack3_03()
    {
        target = toPlayer.normalized;
        yield return new WaitForSeconds(0.5f);

    }
    IEnumerator CloseDefense()
    {
        yield return new WaitForSeconds(2.5f);
        bc.IssueBool("Defense", false);
    }


}

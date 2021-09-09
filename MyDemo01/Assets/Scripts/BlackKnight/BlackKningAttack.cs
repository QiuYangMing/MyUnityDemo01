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
    
    public bool lockBehavior = false;
    private float angel;
    private int candge;
    private float dist;
    private Dictionary<string, float> elements = new Dictionary<string, float>();

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
        //Debug.Log(dist);
        toPlayer = bi.Player.transform.position - transform.position;
        angel = Vector3.Angle(transform.forward, toPlayer);
        if (bi.Player != null && !bi.Player.GetComponent<ActorController>().m_Respawning && dist <=bi.attackRange)
        {
            if (!lockBehavior)
            {
                SequenceNode();
            }
        }
    }

   


    private void SequenceNode()
    {
        if (dist >=10)
        {
            //射击 40%
            elements.Add("Arrow", 0.4f);
            //往前跑60%
            elements.Add("Skill_forward", 0.6f);
            Filter(elements);
        }
        else if (dist >= 6)
        {
            //左徘徊30%
            elements.Add("Skill_walk_L", 0.3f);
            //右徘徊30%
            elements.Add("Skill_walk_R", 0.3f);
            //举盾前冲40%
            elements.Add("Skill_shield_forward", 0.4f);
            Filter(elements);
        }
        else if (dist >= 3)
        {
            //连续突刺4连30%
            elements.Add("Skill_spike_combo", 0.3f);
            //盾牌连击4连30%
            elements.Add("Skill_shield_combo", 0.3f);
            //左徘徊20%
            elements.Add("Skill_walk_L", 0.2f);
            //右徘徊20%
            elements.Add("Skill_walk_R", 0.2f);
            Filter(elements);
        }
        else
        {
            //蓄力一击 35%
            elements.Add("Skill_attack", 0.35f);
            //蓄力直刺 35%
            elements.Add("Skill_spike", 0.35f);
            //民工4连 30%
            elements.Add("Skill_attack_combo", 0.3f);
            Filter(elements);
        }
    }
    private void Filter(Dictionary<string, float> skills)
    {
        System.Random r = new System.Random();
        double diceRoll = r.NextDouble();
        double cumulative = 0.0;
        string selectedElement = null;
        foreach (KeyValuePair<string,float> item in skills)
        {
            cumulative += item.Value;
            if (diceRoll <= cumulative)
            {
                selectedElement = item.Key;
                break;
            }
        }
        if (selectedElement == "Skill_walk_L" || selectedElement == "Skill_walk_R" || selectedElement == "Skill_forward")
        {
            bc.IssueBool(selectedElement, true);
        }
        else if(bc.CheckState("skill_walk_R") || bc.CheckState("skill_forward") || bc.CheckState("skill_walk_L"))
        {
            bc.IssueBool("Skill_forward", false);
            bc.IssueBool("Skill_walk_L", false);
            bc.IssueBool("Skill_walk_R", false);
            skills.Clear();
            return;
        }
        else
        {
            bc.IssueTrigger(selectedElement);
        }
        skills.Clear();

    }

    private void OnSkill_forwardEnter()
    {
        StartCoroutine(LockBehaviorFunction(1.8f));
    }
    private void OnLingerEnter()
    {
        StartCoroutine(LockBehaviorFunction(1.5f));
    }
    private void OnLockBehaviorEnter()
    {
        lockBehavior = true;
    }
    private void OnLockBehaviorExit()
    {

        lockBehavior = false;
    }
    private void OnLockTargetUpdate()
    {
        LockPlayer();
    }
    private void OnRightUpdate()
    {
        bc.rigid.position += transform.right * 0.03f;
    }
    private void OnLeftUpdate()
    {
        bc.rigid.position += transform.right * -0.03f;
    }
    private void OnForwardUpdate()
    {
        bc.rigid.position += transform.forward * 0.03f;
    }
    private void LockPlayer()
    {
        if (bi.Player != null && bc.GetAnim().GetBool("Grounded"))
        {
            Vector3 playerPos = bi.Player.transform.position;
            playerPos.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(playerPos);
            Vector3 fromTarget = transform.position - bi.Player.transform.position;
            fromTarget.y = 0;
            Vector3 requiredPoint = bi.Player.transform.position + fromTarget.normalized * 2f * 0.9f;
            bi.lockon = true;
        }
    }
    IEnumerator LockBehaviorFunction(float lockTime)
    {

        lockBehavior = true;
        yield return new WaitForSeconds(lockTime);
        bc.IssueBool("Skill_forward", false);
        bc.IssueBool("Skill_walk_L", false);
        bc.IssueBool("Skill_walk_R", false);

        lockBehavior = false;
    }
    IEnumerator MoveClose()
    {
        yield return new WaitForSeconds(3f);
        bc.IssueBool("Skill_forward", false);
        bc.IssueBool("Skill_walk_L", false);
        bc.IssueBool("Skill_walk_R", false);
    }




}

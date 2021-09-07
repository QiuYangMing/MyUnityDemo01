using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletion_Archer_AAttack : MonoBehaviour {

    private BlackKnightController bc;
    private MyTime myTime = new MyTime();
    private GameObject ArrowObj;
    private Transform fireTrans;
    private AudioClip audioClip;
    private AudioSource audioS;
    private float dist;

    void Start () {
        bc = GetComponentInParent<BlackKnightController>();
        ArrowObj = Resources.Load<GameObject>("EnArrow");
        fireTrans = GameTool.GetTheChildComponent<Transform>(gameObject, "Bone_Bow_A");
        audioS = bc.transform.Find("AudioSource").GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Audio/AttackEffectMusic/BowArrowEffectMusic");
    }

    // Update is called once per frame
  

    private void FixedUpdate()
    {
        dist = Vector3.Distance(bc.ei.Player.transform.position, transform.position);
        if (bc.ei.Player != null && !bc.ei.Player.GetComponent<ActorController>().m_Respawning)
        {
            myTime.Tick();
            if (bc.canAttack && dist < 10f )
            {
                Attack();
                bc.canAttack = false;
                bc.StartTimer(myTime, 5f);
            }
            if (myTime.state == MyTime.STATE.FINISHED)
            {
                bc.canAttack = true;
                myTime.RestState();
            }

        }
    }
    private void Attack()
    {
        bc.IssueTrigger("Attack");
    }
    public void OnfireArrow()
    {
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                audioS.clip = audioClip;
                audioS.Play();
            }
        }
        
        GameObject arrow = Instantiate(ArrowObj, fireTrans.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        bc.canAttack = true;
    }
    private void OnTriggerExit(Collider other)
    {
        bc.canAttack = false;
    }
}

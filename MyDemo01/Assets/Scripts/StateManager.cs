using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StateManager : IActorMnagerInterface
{
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isConterBack;
    public bool isConterBackTrue;
    public bool isCanfly;
    public bool isAirAttack;
    public float ATK = 10.0f;
	void Start () {

        
    }
   
	

	void Update ()
    {

        isGround = am.ac.CheckState("ground");

        isJump = am.ac.CheckState("JUMP");

        isFall = am.ac.CheckState("fall");

        isRoll = am.ac.CheckState("roll");

        isDie = am.ac.CheckState("die");

        isHit = am.ac.CheckState("hit");

        isAttack = am.ac.CheckStateTag("AttackL");
        isAirAttack = am.ac.CheckStateTag("AirAttack");





    }
    public void AddHp(float value)
    {
        MVC.SendEvent(GameDefine.command_AddHP,value);
       
    }
    
}

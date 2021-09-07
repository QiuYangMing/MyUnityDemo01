using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnStateManager : IActorMnagerInterface
{
    public bool isGround;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCanfly;
    public bool isAttackCombo;
    public bool isshield;
    public bool isDodge;
    public bool isLock;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float HpMax;
    private float ATK;
    public float HP
    {
        set { hp = value; }
        get { return hp; }
    }
    public float HPMax
    {
        set { HpMax = value; }
        get { return HpMax; }
    }
    public float Atk
    {
        set { ATK = value; }
        get { return ATK; }
    }


    void Update () {
        isGround = em.bc.CheckState("ground");
        isAttack = em.bc.CheckStateTag("Attack");
        isAttackCombo = em.bc.CheckState("Attack3");
        isHit = em.bc.CheckStateTag("hit");
        isDie = em.bc.CheckState("Death");
        isshield = em.bc.CheckState("Shield");
        isDodge = em.bc.CheckState("Dodge");
        isDefense = em.bc.CheckState("Defense") || em.bc.CheckState("block");
        isBlocked = em.bc.CheckState("Blocked");
        isLock = em.bc.CheckState("lock");
    }
    public void AddHp(float value)
    {
        if (em.bc.ei.EnName == "BlackKnight")
        {
            MVC.SendEvent(GameDefine.command_EnAddHP, value);
        }
        hp += value;
        hp = Mathf.Clamp(hp, 0, HpMax);
        
    }
}

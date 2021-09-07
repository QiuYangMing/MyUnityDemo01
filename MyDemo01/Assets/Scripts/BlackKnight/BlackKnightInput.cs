using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightInput : EnInput {

   
    private float h;
    private float v;
    private float velocityDup;
    private float velocityDright;
    private float Dup2;
    private float Dright2;
    private int selectAttack;
    private int wakeOrArrow;
    private int fourOrFive;
    
    MyTime myTime = new MyTime();
    MyTime attaclTime = new MyTime();
    protected override void Awake()
    {
        base.Awake();
        bc = GetComponent<BlackKnightController>();
        EnName = "BlackKnight";
        attackRange = 8f;
        battleRange = 30f;

    }
   
}

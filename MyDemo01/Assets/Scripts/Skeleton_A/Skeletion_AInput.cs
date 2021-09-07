using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletion_AInput : EnInput
{

    protected override void Awake()
    {
        base.Awake();
        EnName = "Skeletion_A";
        battleRange = 6f;
        attackRange = 2f;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletion_Archer_AInput : EnInput
{
    protected override void Awake()
    {
        base.Awake();
        EnName = "Skeleton_archer_A";
        battleRange = 10f;
        attackRange = 10f;
    }
    


}

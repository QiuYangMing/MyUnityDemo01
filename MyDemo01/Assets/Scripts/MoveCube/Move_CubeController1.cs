using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_CubeController1 : Move_Cube
{
    protected override void Start()
    {
        base.Start();
        CubeName = "Cube02";
        value01 = -26.5f;
        value02 = -39.22f;
    }
    protected override void FixedUpdate()
    {
        if (GameData.towTirrgger)
        {
        base.FixedUpdate();

        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    protected override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }

}

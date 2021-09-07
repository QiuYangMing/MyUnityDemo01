using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_CubeController : Move_Cube
{
    protected override void Start()
    {
        base.Start();
        CubeName = "Cube01";
        value01 = -8.5f;
        value02 = -19.5f;
    }
    protected override void FixedUpdate()
    {
        if (GameData.oneTirgger)
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

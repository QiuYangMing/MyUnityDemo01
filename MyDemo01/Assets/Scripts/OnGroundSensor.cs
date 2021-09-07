using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour {
    private CapsuleCollider capcol;

    //private Vector3 point1;
    //private Vector3 point2;
    //private float radius;
    //private Vector3 center;
	void Awake () {
        capcol = GetComponent<CapsuleCollider>();
        //radius = capcol.radius;
    }

    // Update is called once per frame

    //   void FixedUpdate() {
    //       point1 = transform.position + transform.up * 0.2f;
    //       point2 = transform.position + transform.up * capcol.height - transform.up * radius;

    //       Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));

    //       if (outputCols.Length != 0)
    //       {
    //           SendMessageUpwards("IsGround");
    //       }
    //       else
    //       {
    //            SendMessageUpwards("IsNotGround");
    //       }

    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SendMessageUpwards("IsGround");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SendMessageUpwards("IsGround");
        }
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SendMessageUpwards("IsNotGround");
        }
    }

}

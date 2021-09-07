using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnTargetScanner : MonoBehaviour {

    private Transform player;
	void Start () {
        player = GameObject.Find("PlayerHandle").transform;
	}
	
	
	void Update ()
    {
        
        Vector3 toplayer = player.position - transform.position;
         //Debug.Log(Vector3.Angle(transform.forward, toplayer));
         Debug.Log(toplayer.magnitude);
        Debug.DrawRay(transform.position,toplayer,Color.green);
	}
}

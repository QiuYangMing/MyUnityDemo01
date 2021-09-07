using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCube01 : MonoBehaviour {

    private Animator anim01;
    private Animator anim02;
    public Rigidbody sphereOne;
    void Start () {
        anim01 = GameObject.Find("RotationCube01").GetComponent<Animator>();
        anim02 = GameObject.Find("RotationCube03").GetComponent<Animator>();
        sphereOne = GameObject.Find("SphereOne(Clone)").GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        anim01.SetBool("rotation", true);
        anim02.SetBool("rotation", true);
        sphereOne.AddForce(Vector3.right);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        anim01.SetBool("rotation", false);
        anim02.SetBool("rotation", false);
    }
}

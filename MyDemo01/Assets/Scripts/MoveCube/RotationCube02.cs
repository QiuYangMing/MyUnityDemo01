using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCube02 : MonoBehaviour {

    private Animator anim01;
    private Animator anim02;
    public Rigidbody sphereTow;
    void Start () {
        anim01 = GameObject.Find("RotationCube02").GetComponent<Animator>();
        anim02 = GameObject.Find("RotationCube04").GetComponent<Animator>();
        sphereTow = GameObject.Find("SphereTow(Clone)").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        anim01.SetBool("rotation", true);
        anim02.SetBool("rotation", true);
        sphereTow.AddForce(Vector3.left);

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

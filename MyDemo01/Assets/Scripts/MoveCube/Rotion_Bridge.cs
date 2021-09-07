using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotion_Bridge : MonoBehaviour {

    public Animator anim;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Magic")
        {
            anim.SetBool("rotaion", true);

        }
    }
}

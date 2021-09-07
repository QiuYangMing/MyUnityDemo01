using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour {

	
	
	
	void Update () {
        transform.Translate(Vector3.forward);
	}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject,0.5f);
    }
}

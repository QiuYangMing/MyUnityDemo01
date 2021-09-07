using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPContorl : MonoBehaviour
{
    private BoxCollider myBox;
    private Vector3 targetTrans;
    void Start()
    {
        myBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.name == "TpGo")
            {
                targetTrans = new Vector3(-0.17f, 18.33f,-186.07f);
            }
            else if (this.name == "TpBack")
            {
                targetTrans = new Vector3(16.72777f, 1.183f, -185.486f);

            }
            other.transform.position = targetTrans;
        }
    }
}

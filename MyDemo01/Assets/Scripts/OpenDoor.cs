using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
   
    private void Update()
    {
        if (GameData.SphereL == 1 && GameData.SphereR == 1)
        {
            transform.position = Vector3.Lerp(transform.position,transform.position+new Vector3(0,6f,0),0.1f);
        }
    }

}

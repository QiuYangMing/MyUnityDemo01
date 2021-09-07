using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameData.oneTirgger)
        {
            GameData.oneTirgger = true;
        }
        else if (GameData.oneTirgger && !GameData.towTirrgger)
        {
            GameData.towTirrgger = true;
        }
        else
        {
            return;
        }
        this.gameObject.SetActive(false);
    }

}

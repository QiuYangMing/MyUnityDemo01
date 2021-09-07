using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFool : MonoBehaviour
{
    private SphereSuccess sphere;
    private void Start()
    {
        if (GameData.leveName == "level3Enemy")
        {
            sphere = GameObject.Find("Pipe01").transform.Find("TiggerTarpR").GetComponent<SphereSuccess>();

        }
        else if (GameData.leveName == "level4Enemy")
        {
            sphere = GameTool.GetTheChildComponent<SphereSuccess>(GameObject.Find("Zeldroom").transform.Find("Pipe01").gameObject, "TiggerTarpR");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ActorManager>().Die();
            GameData.hp = 0;
        }
        if (other.tag == "sphereTarp")
        {
            sphere.CreatSphere(other.name);
            Destroy(other.gameObject);
        }
    }
}

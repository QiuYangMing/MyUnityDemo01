using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSuccess : MonoBehaviour {

    private GameObject sphere01;
    private GameObject sphere02;

    private Transform oneShere;
    private Transform towShere;
    private List<GameObject> spheres = new List<GameObject>();
    private RotationCube01 cube01;
    private RotationCube02 cube02;

    void Start () {
        sphere01 = Resources.Load<GameObject>("SphereOne");
        sphere02 = Resources.Load<GameObject>("SphereTow");

        oneShere = GameObject.Find("OneSphere").transform;
        towShere = GameObject.Find("TowSphere").transform;
        cube01 = GameObject.Find("T_RotationCube01").GetComponent<RotationCube01>();
        cube02 = GameObject.Find("T_RotationCube02").GetComponent<RotationCube02>();

    }
	
	
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "sphereTarp")
        {
            return;
        }
        if (this.name == "TiggerTarpR")
        {
            GameData.SphereR += 1;
            spheres.Add(other.gameObject);
        }
        else if (this.name == "TiggerTarpL")
        {
            GameData.SphereL += 1;
            spheres.Add(other.gameObject);
        }
        if (GameData.SphereR == 2 || GameData.SphereL == 2)
        {
            foreach (GameObject item in spheres)
            {
                Destroy(item);
            }
            CreatSphere("all");
        }
        Debug.Log(GameData.SphereL);
        Debug.Log(GameData.SphereR);

    }
    public void CreatSphere(string _name)
    {
        switch (_name)
        {
            case "SphereOne(Clone)":
                cube01.sphereOne = Instantiate(sphere01, oneShere.position, Quaternion.identity).GetComponent<Rigidbody>();
                break;
            case "SphereTow(Clone)":
                cube02.sphereTow = Instantiate(sphere02, towShere.position, Quaternion.identity).GetComponent<Rigidbody>();
                break;
            default:
                cube01.sphereOne = Instantiate(sphere01, oneShere.position, Quaternion.identity).GetComponent<Rigidbody>();
                cube02.sphereTow = Instantiate(sphere02, towShere.position, Quaternion.identity).GetComponent<Rigidbody>();
                GameData.SphereL = 0;
                GameData.SphereR = 0;
                break;
        }
        
    }
}

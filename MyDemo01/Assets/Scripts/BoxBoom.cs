using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoom : MonoBehaviour
{

    public GameObject[] Chunks;
    private GameObject RedORB;

    private void Start()
    {
        RedORB = Resources.Load<GameObject>("RedORB");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Magic")
        {
            Crushing();

        }
    }
    void Crushing()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        foreach (GameObject chunk in Chunks)
        {
            chunk.SetActive(true);
            chunk.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -200);
            chunk.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * -20 * Random.Range(-5f, 5f));
            chunk.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * -20 * Random.Range(-5f, 5f));
        }
        Instantiate(RedORB, transform.position+transform.forward*3, transform.rotation);
        Invoke("DestructObject", 3);
    }
    void DestructObject()
    {
        Destroy(gameObject);
    }
}

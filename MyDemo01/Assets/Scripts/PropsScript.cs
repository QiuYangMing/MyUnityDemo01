using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsScript : MonoBehaviour
{
    private GameObject parent;
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.tag == "Props_blue")
            {
                List<string> AddProps = new List<string>();
                AddProps.Add("Blue");
                AddProps.Add("1");
                MVC.SendEvent(GameDefine.command_AddUpdateProps, AddProps);
                Destroy(parent);
            }
            else if (this.tag == "Props_green")
            {
                List<string> AddProps = new List<string>();
                AddProps.Add("Green");
                AddProps.Add("1");
                MVC.SendEvent(GameDefine.command_AddUpdateProps, AddProps);
                Destroy(parent);
            }

        }
        return;
    }
}

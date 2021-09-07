using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour {

    private BoxCollider myBox;
	void Start () {
        myBox = GetComponent<BoxCollider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ShowUI(E_UiId.ShopUI);
        Cursor.lockState = CursorLockMode.None;
    }
}

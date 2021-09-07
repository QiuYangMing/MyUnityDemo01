using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRedORB : MonoBehaviour {

    private GameObject Player;
    private ParticleSystem partic;
	void Start () {
        Player = GameObject.Find("PlayerHandle");
        partic = Player.transform.Find("smoke").GetComponent<ParticleSystem>();

    }


    private void FixedUpdate()
    {
        try
        {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.4f);

        }
        catch (System.Exception)
        {

            throw;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            MVC.SendEvent(GameDefine.command_AddRedORB,10);
            partic.Play();
            if (GameData.Win)
            {
                UIManager.Instance.ShowUI(E_UiId.ResetGameUI);
            }
            Destroy(this.gameObject);
            
        }
    }
}

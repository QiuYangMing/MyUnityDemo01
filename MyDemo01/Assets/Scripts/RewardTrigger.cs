using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTrigger : MonoBehaviour
{
    private BoxCollider rewardWall;
    void Start()
    {
        rewardWall = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.name != "RewardTrigger03")
        {

            MVC.SendEvent(GameDefine.command_AddRedORB, 60);
            other.transform.Find("smoke").GetComponent<ParticleSystem>().Play();
            if (GameData.Win)
            {
                UIManager.Instance.ShowUI(E_UiId.ResetGameUI);
            }
            Destroy(this.gameObject);

        }
        else if (other.tag == "Player" && gameObject.name == "RewardTrigger03")
        {
            UIManager.Instance.ShowUI(E_UiId.EnemyInforUI);
            GameData.restPlayer = true;
            StartCoroutine(EnableAirWall());
        }
    }

    IEnumerator EnableAirWall()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}

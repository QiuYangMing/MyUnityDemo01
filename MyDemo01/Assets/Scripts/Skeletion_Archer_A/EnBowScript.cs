using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnBowScript : MonoBehaviour {
    private GameObject hitParticle;
    private ParticleSystem trailParticle;
    private GameObject player;
    private bool PlayerState;
    private AudioManager audioM;
    void Start()
    {
        audioM = GameObject.Find("Canvas(Clone)").transform.Find("UnitySingletonObj").GetComponent<AudioManager>();
        player = GameObject.Find("PlayerHandle");
        hitParticle = Resources.Load<GameObject>("HitParticle");
        trailParticle = GameTool.GetTheChildComponent<ParticleSystem>(gameObject, "TrailParticle");
        Destroy(gameObject, 6f);

    }
    private void FixedUpdate()
    {
        try
        {
         transform.position = Vector3.Lerp(transform.position,player.transform.position,0.4f);

        }
        catch (System.Exception)
        {

            throw;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {

        GameObject hit = Instantiate(hitParticle, transform.position, Quaternion.identity);
        if (other.tag != "Player")
        {
            Debug.Log(other.tag);
            Destroy(gameObject);
            return;
        }
        PlayerState = other.GetComponent<ActorManager>().sm.isConterBack;
        if (GameData.hp > 0 && !PlayerState)
        {
            other.GetComponent<ActorManager>().sm.AddHp(-10f);
            if (GameData.hp > 0)
            {
                other.GetComponent<ActorManager>().Hit();

            }
            else
            {
                other.GetComponent<ActorManager>().Die();
            }
        }
        else if(GameData.hp <= 0)
        {
            if (other.GetComponent<ActorController>().m_Respawning)
            {
                return;
            }
            other.GetComponent<ActorManager>().Die();
        }
        audioM.PlayEffectMusic("BOOMEffectMusic01");

        Destroy(gameObject);
        Destroy(hit, 1.5f);
    }
}

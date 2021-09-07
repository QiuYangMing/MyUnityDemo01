using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    private GameObject hitParticle;
    private ParticleSystem trailParticle;
    private AudioManager audioM;
	void Start ()
    {
        audioM = GameObject.Find("Canvas(Clone)").transform.Find("UnitySingletonObj").GetComponent<AudioManager>();
        hitParticle = Resources.Load<GameObject>("HitParticle");
        trailParticle = GameTool.GetTheChildComponent<ParticleSystem>(gameObject, "TrailParticle");
        Destroy(gameObject, 4f);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Sensor")
        {
            return;
        }
        GameObject hit = Instantiate(hitParticle, transform.position, Quaternion.identity);

        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);
        audioM.PlayEffectMusic("BOOMEffectMusic01");
        Destroy(gameObject);
        Destroy(hit, 1.5f);
        
    }


}

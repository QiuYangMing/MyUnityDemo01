using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

    public BattleManager bm;
    public WeaponManager wm;
    public ActorController ac;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;
    private GameObject modle;
    private AudioSource audioS;
    private AudioClip[] audioClip;
    void Start ()
    {
        ac = GetComponent<ActorController>();
        GameObject sensor = transform.Find("sensor").gameObject;
        modle = ac.model;
        bm = Bind<BattleManager>(sensor);

        wm = Bind<WeaponManager>(modle);

        sm = Bind<StateManager>(gameObject);

        dm = Bind<DirectorManager>(gameObject);

        im = Bind<InteractionManager>(sensor);
        audioS = transform.Find("Audio").GetComponent<AudioSource>();
        audioClip = Resources.LoadAll<AudioClip>("Audio/HitEffectMusic");

        ac.OnAction += DoAction;
    }
	
	// Update is called once per frame
	void Update () {
        ac.IssueBool("conterBackTure", sm.isConterBackTrue);
        
	}
    private T Bind<T>(GameObject go) where T : IActorMnagerInterface
    {
        T tempInstance;
        if (go == null)
        {
            return null;
        }
        tempInstance = go.GetComponent<T>();
        if (tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }
    private void DoAction()
    {
        if (im.overlapEcastms.Count != 0)
        {
            if (im.overlapEcastms[0].eventName == "assasion_01")
            {
                dm.victim = im.overlapEcastms[0].em;
                transform.position = im.overlapEcastms[0].em.transform.position + im.overlapEcastms[0].em.transform.TransformVector(im.overlapEcastms[0].offset);
                ac.model.transform.LookAt(im.overlapEcastms[0].em.transform, Vector3.up);
                dm.PlayFrontStab("assasion_01", this, im.overlapEcastms[0].em);
            }
        }
    }

    public void TryDoDamage(EnWeaponController targetWc,bool attackValid,bool counterValid)
    {

        if (sm.isRoll)
        {
            return;
        }
        else if (sm.isConterBack)
        {
            targetWc.ewm.em.Blocked(this);
        }
        else
        {
            HitOrDie(targetWc, true);
        }

    }

    public void HitOrDie(EnWeaponController targetWc, bool doHitAnimation)
    {
        if (GameData.hp <= 0)
        {
            Die();
            ac.m_Respawning = true;
        }
        else
        {
            StopCoroutine(AudioValue());
            StartCoroutine(AudioValue());
            sm.AddHp(-1 * targetWc.GetATK());
            if (GameData.hp > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                    if (targetWc.ewm.em.esm.isCanfly)
                    {
                        AirFly();
                    }
                }
            }
            else
            {
                Die();
                ac.m_Respawning = true;
            }
        }
    }
    public void SetIsCounterBack(bool value)
    {
        if (value)
        {
        sm.isConterBack = true;

        }
        else
        {
        sm.isConterBack = false;
        }
    }
    public void Stunned()
    {
        ac.IssueTrigger("conterBack");
    }
   
    public void Hit()
    {
        if (sm.isAirAttack)
        {

        }
        else
        {
            ac.IssueTrigger("hit");
        }
        StopCoroutine(ShoundedUI());
        StartCoroutine(ShoundedUI());
    }
    public void Die()
    {
        ac.IssueTrigger("die");

        if (ac.cancom.lockState ==true)
        {
            ac.cancom.LockUnlock();
        }
            ac.cancom.enabled = false;
    }
    void AirFly()
    {
      
        ac.rigid.AddForce(transform.up * 300f);
        
    }
    public void LockUnlockActorController(bool value)
    {
        ac.IssueBool("lockAM", value);
    }
    IEnumerator ShoundedUI()
    {
        UIManager.Instance.ShowUI(E_UiId.WoundedUI);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.HideSingleUI(E_UiId.WoundedUI);
    }
    IEnumerator AudioValue()
    {
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                audioS.clip = audioClip[Random.Range(0, 3)];
                audioS.volume = 0.4f;
                audioS.Play();
            }
        }
        yield return new WaitForSeconds(1f);
        audioS.volume = 1f;

    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour {

    public EnBattleManager ebm;
    public EnWeaponManager ewm;
    public BlackKnightController bc;
    public EnStateManager esm;
    public DirectorManager dm;
    public InteractionManager im;
    public EventCasterManager ecm;
    public GameObject modle;
    public GameObject caster;
    public GameObject sensor;
    public ParticleSystem eparticle;
    private int hitNum;
    public AudioSource audioS;
    private AudioClip[] audioClip;

    [Header("打击感参数")]
    private float shakeTime = 0.1f;
    private int lightPause = 6 ;
    private float lightStrength = 0.015f;
    private int heavyPause = 12;
    private float heavyStrength = 0.065f;

    public float GetShakeTime()
    {
        return shakeTime;
    }
    public float GetLightStrength()
    {
        return lightStrength;
    }

    void Start () {
        bc = GetComponent<BlackKnightController>();
        sensor = transform.Find("sensor").gameObject;
        caster = transform.Find("caster").gameObject;
        modle = bc.modle;
        if (bc.ei.EnName != "Skeleton_archer_A")
        {
        ewm = Bind<EnWeaponManager>(modle);
        eparticle = ewm.transform.Find("blood02").GetComponentInChildren<ParticleSystem>();
        }
        else
        {
            eparticle = transform.Find("Skeleton_archer_A").Find("blood02").GetComponentInChildren<ParticleSystem>();
        }
        ebm = Bind<EnBattleManager>(sensor);
        esm = Bind<EnStateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);
        ecm = Bind<EventCasterManager>(caster);
        hitNum = 0;
        audioS = transform.Find("AudioSource").GetComponent<AudioSource>();
        audioClip = Resources.LoadAll<AudioClip>("Audio/HitEffectMusic");
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
        tempInstance.em = this;
        return tempInstance;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        if (bc.ei.EnName == "BlackKnight")
        {
            if (esm.isBlocked)
            {
                HitOrDie(targetWc, false);
            }
            else if (esm.isDodge)
            {
                return;
            }
            else if(esm.isshield)
            {
                HitOrDie(targetWc, false);
            }
            else if (esm.isDefense)
            {
                Hit();
                if (targetWc.wm.am.sm.isAirAttack)
                {
                    FindObjectOfType<HitStop>().Stop(0.1f);
                }

            }
            else
            {
                HitOrDie(targetWc, hitNum >= 3 );
                if (targetWc.wm.am.sm.isAirAttack)
                {
                    FindObjectOfType<HitStop>().Stop(0.1f);
                }
                
            }
            return;
        }
        if (esm.isshield)
        {
            HitOrDie(targetWc, false);
        }
        if (esm.isDefense)
        {
            Hit();
            if (targetWc.wm.am.sm.isAirAttack)
            {
                //FindObjectOfType<HitStop>().Stop(0.1f);
                //AttackScene.Instance.HitPaues(targetWc.wm.am.ac.GetAnim(), 0, lightPause);
                //AttackScene.Instance.HitPaues(bc.GetAnim(), 0, lightPause);
                //AttackScene.Instance.CameraShake(shakeTime, lightStrength);
            }

        }
        else
        {
            HitOrDie(targetWc, true);
            if (targetWc.wm.am.sm.isAirAttack)
            {
                //FindObjectOfType<HitStop>().Stop(0.1f);
                //AttackScene.Instance.HitPaues(targetWc.wm.am.ac.GetAnim(), 0, lightPause);
                //AttackScene.Instance.HitPaues(bc.GetAnim(), 0, lightPause);
                //AttackScene.Instance.CameraShake(shakeTime, lightStrength);


            }

        }
    }
    public void HitOrDie(WeaponController targetWc , bool doHitAnimation)
    {
        if (esm.HP <= 0)
        {
            Die();
        }
        else
        {
            hitNum += 1;
            esm.AddHp(-1 * targetWc.GetATK());
            if (GameTool.HasKey("isCloseAudio"))
            {
                if (!bool.Parse(GameTool.GetString("isCloseAudio")))
                {
                    audioS.clip = audioClip[Random.Range(4, 8)];
                    audioS.Play();
                }
            }
            eparticle.Play();
            if (esm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                    if (targetWc.wm.am.sm.isCanfly)
                    {

                        StartCoroutine(AirFly(targetWc));
                    }
                    else 
                    {
                        AttackScene.Instance.HitPaues(targetWc.wm.am.ac.GetAnim(), 0, lightPause);
                        AttackScene.Instance.HitPaues(bc.GetAnim(), 0, lightPause);
                        AttackScene.Instance.CameraShake(shakeTime, lightStrength);

                    }

                }
            }
            else
            {
                Die();
            }
        }
    }

    public void AsssionDamege(float damege)
    {
        esm.AddHp(damege);
        if (esm.HP <= 0)
        {
            Die();
        }


    }
    public void SetIsCounterBack(bool value)
    {

    }
    public void Blocked( ActorManager am)
    {
       
        bc.IssueTrigger("Blocked");
        caster.SetActive(true);
        caster.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(OpenOrCloseBack(am));
    }
    public void Hit()
    {

        bc.IssueTrigger("Hit");
        hitNum = 0;

    }
    public void Die()
    {
        bc.IssueTrigger("Death");
    }

    public void lockUnlockEm(bool value)
    {
        bc.IssueBool("LockEM", value);
    }
    IEnumerator AirFly(WeaponController wc)
    {
        bc.rigid.mass = 1;
        bc.rigid.AddForce(transform.up*350f);
        yield return new WaitForSeconds(2.5f);
        bc.rigid.mass = 20;
        bc.rigid.useGravity = true;
        bc.rigid.isKinematic = false;
        yield return new WaitForSeconds(1f);
        bc.rigid.useGravity = true;
        bc.rigid.isKinematic = false;
    }
    IEnumerator OpenOrCloseBack(ActorManager am)
    {
        am.sm.isConterBackTrue = true;
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                audioS.clip = audioClip[8];
                audioS.volume = 1;
                audioS.Play();
            }
        }
        
        yield return new WaitForSeconds(1.8f);
        am.sm.isConterBackTrue = false;
       

    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class victimBehaviour : PlayableBehaviour
{
    public EnemyManager em;
    public PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {

    }
    public override void OnGraphStop(Playable playable)
    {
        
    }
   
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        em.lockUnlockEm(true);
        em.StartCoroutine(Damege());
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        em.lockUnlockEm(false);
    }

    

    IEnumerator Damege()
    {
        yield return new WaitForSeconds(1f);
        if (GameTool.HasKey("isCloseAudio"))
        {
            if (!bool.Parse(GameTool.GetString("isCloseAudio")))
            {
                em.audioS.clip = em.audioClip[UnityEngine.Random.Range(4, 8)];
                em.audioS.Play();
            }
        }
        AttackScene.Instance.CameraShake(0.3f, 0.05f);
        yield return new WaitForSeconds(2.5f);
        em.AsssionDamege(-60f);
        if (em.esm.HP <= 0)
        {
            yield return new WaitForSeconds(1f);
            pd.Stop();
        }
        
    }

}

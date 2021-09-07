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
        yield return new WaitForSeconds(3.5f);
        em.AsssionDamege(-60f);
        if (em.esm.HP <= 0)
        {
            yield return new WaitForSeconds(1f);
            pd.Stop();
        }
        
    }

}

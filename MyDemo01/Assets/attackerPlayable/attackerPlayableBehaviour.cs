using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class attackerPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public PlayableDirector pd;
    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();
        //foreach (var track in pd.playableAsset.outputs)
        //{
        //    if (track.streamName == "Attacker Animation")
        //    {
        //        am = pd.gameObject.GetComponentInParent<ActorManager>();
        //        am.LockUnlockActorController(true);
               
        //    }
        //}
        
    }
    public override void OnGraphStop(Playable playable)
    {
        am.dm.pd.playableAsset = null;
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        am.LockUnlockActorController(true);
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        am.LockUnlockActorController(false);
        am.im.ClearOverlap();
        

    }
  
}

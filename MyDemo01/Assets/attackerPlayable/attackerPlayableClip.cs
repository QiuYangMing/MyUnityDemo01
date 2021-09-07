using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class attackerPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public attackerPlayableBehaviour template = new attackerPlayableBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<attackerPlayableBehaviour>.Create (graph, template);
        attackerPlayableBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}

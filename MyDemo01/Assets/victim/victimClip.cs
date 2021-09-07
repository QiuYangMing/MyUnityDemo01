using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class victimClip : PlayableAsset, ITimelineClipAsset
{
    public victimBehaviour template = new victimBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<victimBehaviour>.Create (graph, template);
        victimBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}

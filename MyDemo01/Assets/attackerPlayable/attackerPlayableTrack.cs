using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0.9150943f, 0.05758764f)]
[TrackClipType(typeof(attackerPlayableClip))]
[TrackBindingType(typeof(ActorManager))]
public class attackerPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<attackerPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.2297449f, 1f, 0f)]
[TrackClipType(typeof(victimClip))]
[TrackBindingType(typeof(EnemyManager))]
public class victimTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<victimMixerBehaviour>.Create (graph, inputCount);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorMnagerInterface
{
    public PlayableDirector pd;

    [Header("=== Timeline assets ===")]
    public TimelineAsset assassin_01;

    [Header("=== Assets Settings ===")]
    public ActorManager attacker;
    public EnemyManager victim;
    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        attacker = GameObject.Find("PlayerHandle").GetComponent<ActorManager>();
        assassin_01 = Resources.Load<TimelineAsset>("TimeLine/assassin_01");
        
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K) && gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    pd.Play();
        //}
    }
    public bool CheckPlayState()
    {
        if (pd.state == PlayState.Playing)
        {
            return true;
        }
        return false;
    }

    public void PlayFrontStab(string timeLineName, ActorManager attacker, EnemyManager victim)
    {
        if (CheckPlayState())
        {
            return;
        } 
        if (timeLineName == "assasion_01")
        {
            pd.playableAsset = Instantiate(assassin_01);
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;
            ErgodicPlayableAsset(timeline);
            pd.Play();
        }
    }

    private void ErgodicPlayableAsset(TimelineAsset timeline)
    {
        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Attacker Animation")
            {
                pd.SetGenericBinding(track, attacker.ac.GetAnim());
            }
            else if (track.name == "Victim Animation")
            {
                pd.SetGenericBinding(track, victim.bc.GetAnim());

            }
            else if (track.name == "Attacker Playable Track")
            {
                pd.SetGenericBinding(track, attacker);
                foreach (var clip in track.GetClips())
                {
                    attackerPlayableClip attackerPlayable = (attackerPlayableClip)clip.asset;
                    attackerPlayableBehaviour attackerbehav = attackerPlayable.template;
                    attackerbehav.am = attacker;
                    attackerbehav.pd = pd;
                }
            }
            else if (track.name == "Victim Track")
            {
                pd.SetGenericBinding(track, victim);
                foreach (var clip in track.GetClips())
                {
                    victimClip victimPlayable = (victimClip)clip.asset;
                    victimBehaviour victimbehav = victimPlayable.template;
                    victimbehav.em = victim ;
                    victimbehav.pd = pd;
                                        
                }
            }
        }
    }
}
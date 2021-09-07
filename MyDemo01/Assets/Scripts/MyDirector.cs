using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MyDirector : MonoBehaviour
{
    public Animator attacker;
    public Animator victim;
    public PlayableDirector pd;
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        attacker = GameTool.GetTheChildComponent<Animator>(GameObject.Find("PlayerHandle"), "unitychan");
        victim = GameTool.GetTheChildComponent<Animator>(GameObject.Find("EnemyHandle"), "c2410out");
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Animation")
            {
                pd.SetGenericBinding(track.sourceObject, attacker);
            }
            else if (track.streamName == "Victim Animation")
            {
                pd.SetGenericBinding(track.sourceObject, victim);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            pd.Play();
        } 
    }
}

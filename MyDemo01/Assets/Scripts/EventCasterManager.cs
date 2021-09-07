using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorMnagerInterface
{
    public string eventName;
    public bool active;
    public Vector3 offset = new Vector3(0, 0, 1);
    private void Start()
    {
        eventName = "assasion_01";
        active = false;
    }
}

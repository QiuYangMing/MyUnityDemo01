using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorMnagerInterface
{

    private CapsuleCollider interCol;
    public List<EventCasterManager> overlapEcastms = new List<EventCasterManager>();

    private void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EventCasterManager[] eventCasters = other.GetComponents<EventCasterManager>();
        foreach (EventCasterManager eventCaster in eventCasters)
        {
            if (!overlapEcastms.Contains(eventCaster))
            {
                overlapEcastms.Add(eventCaster);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        EventCasterManager[] eventCasters = other.GetComponents<EventCasterManager>();
        foreach (EventCasterManager eventCaster in eventCasters)
        {
            if (overlapEcastms.Contains(eventCaster))
            {
                overlapEcastms.Remove(eventCaster);
            }
        }
    }
    public void ClearOverlap()
    {
        if (overlapEcastms.Count != 0)
        {
            overlapEcastms.Clear();
        }
    }
}

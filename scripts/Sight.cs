using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public string targetTag = "Player";
    public Agent agentData;
    public bool spottedplayer;

    private void Awake()
    {
        agentData = GetComponentInParent<Agent>();
    }
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject target = other.gameObject;
        string tag = target.tag;
        if (tag.Equals(targetTag) == false)
        {
            return;
        }
        agentData.target = target;
        spottedplayer = true;
        return;
    }

    private void OnTriggerExit(Collider other)
    {
        if(agentData.target!=null && other.gameObject == agentData.target)
        {
            agentData.target = null;
            spottedplayer = false;
        }
    }
}

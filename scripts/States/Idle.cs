using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{   
    public override void OnEnable()
    {
        base.OnEnable();
        agent.target = null;
    }
}

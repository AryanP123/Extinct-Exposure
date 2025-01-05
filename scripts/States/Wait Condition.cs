using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeCondition", menuName = "SVS_AI/Conditions/TimeCondition")]
public class WaitCondition : Condition
{
    public float timeTOWait = 2f, timePassed = 0;

    public override bool Test(Agent agent)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeTOWait)
        {
            timePassed = 0;
            return true;
        }
        return false;
    }
}
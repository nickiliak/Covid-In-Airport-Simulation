using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckIfWeActivateBehavior : BehaviorTree.Node
{
    string keyForBool;

    public CheckIfWeActivateBehavior(string keyforBool, float chanceProb)
    {

        keyForBool = keyforBool;
        if (Random.value < chanceProb) SetData(keyForBool, true);
        else SetData(keyForBool, false);
    }

    public override NodeState Evaluate()
    {
        //if parent data is not null it means its brother object has it to false therefore its not null
        if ((bool)GetData(keyForBool) && parent.GetData(keyForBool) == null) 
            return NodeState.SUCCESS;
        else 
            return NodeState.FAILURE;
    }
}

using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AgentBehavior : BehaviorTree.Node
{
    public NavMeshAgent navmeshAgent;
    public string keyForBool = "null";

    public enum InnerState { EXECUTING, WAITING }
    public InnerState Istate = InnerState.EXECUTING;

    public List<string> positionStrings;
    public List<float> waitTimes;

    public float TimeStartedWaiting;

    public bool HasPath()
    {
        return navmeshAgent.hasPath;
    }

    public bool IsCloseEnoughToTarget(int distance)
    {
        return navmeshAgent.remainingDistance < distance;
    }

    public Vector3 FindPositionOfGameObject(string objectName)
    {
        return GameObject.Find(objectName).transform.position;
    }

    public void AgentGoToPosition(Vector3 Position)
    {
        //NavMeshPath path = new NavMeshPath();
        //navmeshAgent.CalculatePath(Position, path);
        //navmeshAgent.SetPath(path);
        navmeshAgent.destination = Position;
    }

    public virtual void ExecuteCustomBehavior() { }
    public NodeState RunNextSetInBehavior(bool executeCustomBeh, int distanceUntilCloseEnough)
    {
        if (waitTimes.Count == 0) return NodeState.FAILURE;

        if (Istate == InnerState.EXECUTING)
        {
            if (HasPath() == false)
                AgentGoToPosition(FindPositionOfGameObject(positionStrings[0]));
            else if (HasPath() == true && IsCloseEnoughToTarget(distanceUntilCloseEnough))
            {
                if (executeCustomBeh) ExecuteCustomBehavior();

                navmeshAgent.ResetPath();
                positionStrings.RemoveAt(0);
                TimeStartedWaiting = Time.time;
                Istate = InnerState.WAITING;
            }

        }
        else if (Istate == InnerState.WAITING)
        {
            if (Time.time - waitTimes[0] > TimeStartedWaiting)
            {
                waitTimes.RemoveAt(0);
                if (waitTimes.Count == 0)
                {
                    parent.SetData(keyForBool, false);
                    return NodeState.SUCCESS;
                }
                else Istate = InnerState.EXECUTING;
            }
        }

        return NodeState.RUNNING;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStateMachine;
using UnityEngine.AI;

//Script for moving enemy until it finds player then it will switch state to EnemyHuntState script
public class EnemyPatrolState : State<Enemy>
{
    //uses singleton pattern to ensure only one instance of script is returned
    private static EnemyPatrolState _singleton;
    private Enemy owner = null;
    private Transform[] patrolPoints;
    private NavMeshAgent agent;

    private EnemyPatrolState()
    {
        if (_singleton != null)
        {
            return;
        }
        _singleton = this;
    }

    public static EnemyPatrolState Singleton
    {
        get
        {
            if (_singleton == null)
            {
                new EnemyPatrolState();
            }
            return _singleton;
        }
    }
    public override void EnterState(Enemy _owner)
    {
        owner = _owner;
        Debug.Log("Enemy State-> Patrol");
        patrolPoints = owner.getLocations();
        agent = owner.GetComponent()<NavMeshAgent>;
    }
    public override void ExitState(Enemy _owner)
    {
        owner = null;
    }

    //events to change state are....has cone collided with player, if so then do a ray cast at that point, is the first object hit by ray player, if yes hunt, else keep patroling
    public override void UpdateState(Enemy _owner)
    {
        // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
    }
   
    function GotoNextPoint() {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;
            
        // Set the agent to go to the currently selected destination.
        agent.destination = patrolPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

}
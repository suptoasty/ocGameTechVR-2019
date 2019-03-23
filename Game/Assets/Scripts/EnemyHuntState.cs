using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStateMachine;
using UnityEngine.AI;

//script for hunting the player after switching from State from EnemyPatrolState
public class EnemyHuntState : State<Enemy>
{
    //uses singleton pattern to ensure only one instance of script is returned
    private static EnemyHuntState _singleton;
    private EnemyHuntState()
    {
        if (_singleton != null)
        {
            return;
        }
        _singleton = this;
    }

    public static EnemyHuntState Singleton
    {
        get
        {
            if (_singleton == null)
            {
                new EnemyHuntState();
            }
            return _singleton;
        }
    }

    public override void EnterState(Enemy _owner)
    {
        Debug.Log("Enemy State-> Hunt");
        _owner.GetComponent<NavMeshAgent>().enabled = true;
    }
    public override void ExitState(Enemy _owner)
    {
        _owner.GetComponent<NavMeshAgent>().enabled = false;
    }
    public override void UpdateState(Enemy _owner)
    {
        _owner.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        _owner.GetComponent<NavMeshAgent>().SetDestination(_owner.getTarget().transform.position);
    }

    public void shootAtPlayer()
    {
        Ray ray = new Ray();
        ray.origin = this.transform.position;
        ray.direction = targetPlayerController.transform.position - this.transform.position;

        //random # gen for range

        // shoot, calling from EnemyProjectile
        
    }
}
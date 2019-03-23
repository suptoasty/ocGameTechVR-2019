using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStateMachine;

//Script for moving enemy until it finds player then it will switch state to EnemyHuntState script
public class EnemyPatrolState : State<Enemy>
{
    //uses singleton pattern to ensure only one instance of script is returned
    private static EnemyPatrolState _singleton;
    private Enemy owner = null;

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
    }
    public override void ExitState(Enemy _owner)
    {
        owner = null;
    }

    //events to change state are....has cone collided with player, if so then do a ray cast at that point, is the first object hit by ray player, if yes hunt, else keep patroling
    public override void UpdateState(Enemy _owner)
    {

    }

}
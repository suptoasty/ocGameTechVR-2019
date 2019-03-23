using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStateMachine;

//Script for moving enemy until it finds player then it will switch state to EnemyHuntState script
public class EnemyPatrolState : State<Enemy>
{
    //uses singleton pattern to ensure only one instance of script is returned
    private static EnemyPatrolState _singleton;

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

    }
    public override void ExitState(Enemy _owner)
    {

    }
    public override void UpdateState(Enemy _owner)
    {

    }

}
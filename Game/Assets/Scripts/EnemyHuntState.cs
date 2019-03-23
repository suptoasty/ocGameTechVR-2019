using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UStateMachine;

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

    }
    public override void ExitState(Enemy _owner)
    {

    }
    public override void UpdateState(Enemy _owner)
    {
        
    }

}
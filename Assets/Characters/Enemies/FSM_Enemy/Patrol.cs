using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Patrol", menuName = "StatesSO/Patrol")]
public class Patrol : EnemyStateSO
{
    public float patrolSpeed = 2f;
    public override void OnStateEnter(Enemy e)
    {
        e.Pathfinding.StartPatrol();
    }
    public override void OnStateUpdate(Enemy e)
    {
        e.Pathfinding.ContinuePatrol();
    }

    public override void OnStateExit(Enemy e)
    {
        e.Pathfinding.StopPatrol();

    }

}
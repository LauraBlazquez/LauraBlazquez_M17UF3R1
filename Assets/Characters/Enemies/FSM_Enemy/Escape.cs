using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Escape", menuName = "StatesSO/Escape")]
public class Escape : EnemyStateSO
{
    public float escapeSpeed = 4f;
    public float escapeDistance = 10f;

    public override void OnStateEnter(Enemy e)
    {
        e.Pathfinding.Escape();
    }

    public override void OnStateUpdate(Enemy e)
    {
        e.Pathfinding.ContinueMove(escapeSpeed);
    }

    public override void OnStateExit(Enemy e)
    {
        e.Pathfinding.StopEscape();
    }
}
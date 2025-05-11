using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chase", menuName = "StatesSO/Chase")]
public class Chase : EnemyStateSO
{
    public float chaseSpeed = 3.5f;

    public override void OnStateEnter(Enemy e)
    {
    }
    public override void OnStateUpdate(Enemy e)
    {
        if (e.target != null)
        {
            e.MoveToward(e.target.transform.position, chaseSpeed);
        }
    }

    public override void OnStateExit(Enemy e)
    {
    }

}
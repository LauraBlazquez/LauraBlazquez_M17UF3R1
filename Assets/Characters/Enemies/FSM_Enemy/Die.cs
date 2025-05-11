using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Die", menuName = "StatesSO/Die")]
public class Die : EnemyStateSO
{
    public override void OnStateEnter(Enemy e)
    {
        e.Die();
    }

    public override void OnStateUpdate(Enemy e)
    {

    }

    public override void OnStateExit(Enemy e)
    {

    }
}
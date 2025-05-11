using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack", menuName = "StatesSO/Attack")]
public class Attack : EnemyStateSO
{
    public override void OnStateEnter(Enemy e)
    {
    }

    public override void OnStateUpdate(Enemy e)
    {
        if (e.target != null && e.CanFire())
        {
            Vector3 shootPos = e.transform.position + e.transform.forward * 1.2f + Vector3.up;
            Quaternion shootRot = Quaternion.LookRotation(e.target.transform.position - shootPos);
            e.Fire(shootPos, shootRot);
        }
    }

    public override void OnStateExit(Enemy e)
    {
    }
}
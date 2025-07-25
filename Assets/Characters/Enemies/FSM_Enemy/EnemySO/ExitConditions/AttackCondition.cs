using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackCondition", menuName = "EnemyConditionSO/Attack")]
public class AttackCondition : EnemyConditionSO
{
    public override bool CheckCondition(Enemy e)
    {
        return e.OnAttackRange;
    }
}
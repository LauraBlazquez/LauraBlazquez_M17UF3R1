using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "DieCondition", menuName = "EnemyConditionSO/Die")]
public class DieCondition : EnemyConditionSO
{
    public override bool CheckCondition(Enemy e)
    {
        return e.health <= 0;
    }
}
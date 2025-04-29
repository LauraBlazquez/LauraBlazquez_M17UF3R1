using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "DieCondition", menuName = "EnemyConditionSO/Die")]
public class DieCondition : EnemyConditionSO
{
    public override bool CheckCondition(EnemyController ec)
    {
        return ec.HP <= 0;
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseCondition", menuName = "EnemyConditionSO/Chase")]
public class ChaseCondition : EnemyConditionSO
{
    public override bool CheckCondition(Enemy e)
    {
        return e.OnVisionRange;
    }
}
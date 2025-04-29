using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "EscapeCondition", menuName = "EnemyConditionSO/Escape")]
public class EscapeCondition : EnemyConditionSO
{
    public override bool CheckCondition(EnemyController ec)
    {
        if (ec.HP <= 50)
        {
            return ec.escape == true;
        }
        return false;
    }
}
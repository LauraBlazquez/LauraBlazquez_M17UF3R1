using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "EscapeCondition", menuName = "EnemyConditionSO/Escape")]
public class EscapeCondition : EnemyConditionSO
{
    public override bool CheckCondition(Enemy e)
    {
        if (e.health <= 50)
        {
            return e.escape == true;
        }
        return false;
    }
}
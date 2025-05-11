using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EnemyConditionSO : ScriptableObject
{
    public bool answer = true;
    public abstract bool CheckCondition(Enemy e);
}
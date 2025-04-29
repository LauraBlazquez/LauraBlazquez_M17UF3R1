using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateSO : ScriptableObject
{
    public EnemyConditionSO StartCondition;
    public List<EnemyConditionSO> EndConditions;
    public abstract void OnStateEnter(EnemyController ec);
    public abstract void OnStateUpdate(EnemyController ec);
    public abstract void OnStateExit(EnemyController ec);
}
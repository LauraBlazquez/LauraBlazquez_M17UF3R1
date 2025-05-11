using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateSO : ScriptableObject
{
    public EnemyConditionSO StartCondition;
    public List<EnemyConditionSO> EndConditions;
    public abstract void OnStateEnter(Enemy e);
    public abstract void OnStateUpdate(Enemy e);
    public abstract void OnStateExit(Enemy e);
}
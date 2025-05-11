using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float health = 100f;
    public float detectionRange = 8f;
    public float fireRate = 1f;
    public float attackRange = 2f;
    public GenericPool pool;
    private float fireCooldown;

    [Header("References")]
    public GameObject target;
    public Animator animator;
    public EnemyFOV EnemyFOV;
    public EnemyPathFinding Pathfinding;
    public List<GameObject> itemsToHideOnDeath;

    [Header("Flags")]
    public bool OnVisionRange = false;
    public bool OnAttackRange = false;
    public bool escape = false;

    [Header("AI FSM")]
    public EnemyStateSO currentSOState;
    public List<EnemyStateSO> States;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnemyFOV = GetComponent<EnemyFOV>();
        Pathfinding = GetComponent<EnemyPathFinding>();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (target != null)
        {
            OnAttackRange = Vector3.Distance(transform.position, target.transform.position) < attackRange;
        }
        else
        {
            OnAttackRange = false;
        }
        currentSOState?.OnStateUpdate(this);
    }

    public void CheckEndingConditions()
    {
        foreach (EnemyConditionSO condition in currentSOState.EndConditions)
        {
            if (condition.CheckCondition(this) == condition.answer)
            {
                ExitCurrentNode();
                break;
            }
        }
    }

    public void ExitCurrentNode()
    {
        foreach (EnemyStateSO stateSO in States)
        {
            if (stateSO.StartCondition == null || stateSO.StartCondition.CheckCondition(this) == stateSO.StartCondition.answer)
            {
                EnterNewState(stateSO);
                break;
            }
        }
        currentSOState.OnStateEnter(this);
    }

    private void EnterNewState(EnemyStateSO state)
    {
        currentSOState.OnStateExit(this);
        currentSOState = state;
        currentSOState.OnStateEnter(this);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 50f)
        {
            escape = true;
        }

        if (health <= 0)
        {
            Die();
        }
        else
        {
            CheckEndingConditions();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        foreach (var obj in itemsToHideOnDeath)
            obj.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
            Pathfinding.target = target;
            OnVisionRange = true;
            lastPlayerPosition = EnemyFOV.GetLastPlayerPosition(target);
            CheckEndingConditions();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && EnemyFOV.CheckPlayerInVision(other.gameObject))
        {
            lastPlayerPosition = EnemyFOV.GetLastPlayerPosition(other.gameObject);
            CheckEndingConditions();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnVisionRange = false;
            OnAttackRange = false;
            CheckEndingConditions();

            if (health < 100f)
            {
                Pathfinding.ContinuePatrol();
            }
        }
    }

    public virtual void MoveToward(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        dir.y = 0f;
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public bool CanFire()
    {
        return fireCooldown <= 0f;
    }

    public void Fire(Vector3 shootOrigin, Quaternion shootRotation)
    {
        if (pool == null)
        {
            Debug.LogWarning("No pool assigned!");
            return;
        }

        GameObject bullet = pool.GetBullet();
        bullet.transform.position = shootOrigin;
        bullet.transform.rotation = shootRotation;
        bullet.SetActive(true);
        fireCooldown = 1f / fireRate;
    }
}
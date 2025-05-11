using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyPathFinding : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public float patrolRadius = 10f;
    public float escapeDistance = 10f;
    public bool patrullanding = true;

    void Start()
    {
        StartPatrol();
    }

    public void StartPatrol()
    {
        patrullanding = true;
        GoToRandomPoint();
    }

    public void ContinuePatrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GoToRandomPoint();
        }
    }

    public void StopPatrol()
    {
        patrullanding = false;
        agent.ResetPath();
    }

    private void GoToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void Chase()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    public void StopChase()
    {
        agent.ResetPath();
    }

    public void Escape()
    {
        if (target == null) return;
        Vector3 directionAway = (transform.position - target.transform.position).normalized;
        Vector3 escapePoint = transform.position + directionAway * escapeDistance;
        if (NavMesh.SamplePosition(escapePoint, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void ContinueMove(float speed)
    {
        agent.speed = speed;
    }

    public void StopEscape()
    {
        agent.ResetPath();
    }
}
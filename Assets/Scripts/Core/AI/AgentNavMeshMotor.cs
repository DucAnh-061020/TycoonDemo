using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentNavMeshMotor : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = 0.15f;
        agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    }

    /// <summary>
    /// High-performance navigation routine. Fires the path calculator exactly once.
    /// </summary>
    public IEnumerator MoveToTargetRoutine(Vector3 destination, float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
        agent.SetDestination(destination);

        while (agent.pathPending)
        {
            yield return null;
        }

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        agent.isStopped = true;
    }

    public void DisableSimulation()
    {
        agent.isStopped = true;
    }
}
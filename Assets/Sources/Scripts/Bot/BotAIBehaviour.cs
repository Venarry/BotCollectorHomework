using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotAIBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 target)
    {
        _agent.SetDestination(target);
    }
}

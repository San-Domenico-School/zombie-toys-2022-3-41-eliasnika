using UnityEngine;
using UnityEngine.AI; // Import NavMesh functionality

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Transform target; // Assign target in Inspector
    [SerializeField] private float speed; // Adjustable movement speed
    private NavMeshAgent agent; // NavMeshAgent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        speed = agent.speed; // Set speed from NavMeshAgent
    }

    private void SetDestination()
    {
        agent.SetDestination(target.position); // Move toward the target
    }

    void Update()
    {
        SetDestination(); // Continuously update destination
    }
}

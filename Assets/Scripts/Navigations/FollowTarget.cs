using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
	public Transform target;
	Vector3 destination;
	NavMeshAgent agent;

	void Start()
	{
		// Cache agent component and destination
		agent = GetComponent<NavMeshAgent>();
		destination = agent.destination;
	}

	void Update()
	{
		// Update destination if the target moves one unit
			destination = target.position;
			agent.destination = destination;
	}
}
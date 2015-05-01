using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour 
{

    public Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        agent.SetDestination(target.position);

        /*
        if ((Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance) && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            Destroy(gameObject);
        }*/
	}
}

using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour 
{

    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;

    NavMeshAgent agent;

	// Use this for initialization
	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (gameObject.transform.position.z < 40)
        {
            agent.SetDestination(target1.position);
        }
        else if (gameObject.transform.position.z > 90)
        {
            agent.SetDestination(target3.position);
        }
        else if (gameObject.transform.position.x > 30)
        {
            agent.SetDestination(target4.position);
        }
        else
        {
            agent.SetDestination(target2.position);
        }

        
	}
}

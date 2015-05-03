using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour 
{

    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;

    private bool notSet = true;
    private Transform pos;

    NavMeshAgent agent;

	// Use this for initialization
	void Start () 
    {
        pos = gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (notSet)
        {
            if (pos.position.x < -130 && pos.position.z < -65)
            {
                int ran = Random.Range(0, 2);
                if (ran == 0)
                {
                    agent.SetDestination(target1.position);
                    notSet = false;
                }
                else
                {
                    agent.SetDestination(target2.position);
                    notSet = false;

                }
            }
            else if (pos.position.x > 200)
            {
                agent.SetDestination(target3.position);
                notSet = false;
            }
            else if (pos.position.z > 130)
            {
                agent.SetDestination(target4.position);
                notSet = false;
            }
            else if (pos.position.x < -140)
            {
                agent.SetDestination(target1.position);
                notSet = false;
            }
            else
            {
                agent.SetDestination(target2.position);
                notSet = false;
            }
        }

        
	}
}

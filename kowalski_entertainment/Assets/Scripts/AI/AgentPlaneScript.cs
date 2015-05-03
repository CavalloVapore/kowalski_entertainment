using UnityEngine;
using System.Collections;

public class AgentPlaneScript : MonoBehaviour 
{

    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public Transform target5;
    public Transform target6;
    public Transform target7;
    public Transform target8;

    public int chooseTarget = 0;

    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (chooseTarget-1)
        {
            case (0):
                agent.SetDestination(target1.position);
                break;

            case (1):
                agent.SetDestination(target2.position);
                break;

            case (2):
                agent.SetDestination(target3.position);
                break;

            case (3):
                agent.SetDestination(target4.position);
                break;

            case (4):
                agent.SetDestination(target5.position);
                break;

            case (5):
                agent.SetDestination(target6.position);
                break;

            case (6):
                agent.SetDestination(target7.position);
                break;

            case (7):
                agent.SetDestination(target8.position);
                break;
        }
    }
}

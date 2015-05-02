using UnityEngine;
using System.Collections;

public class SpawnAirplane : MonoBehaviour {

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public Transform spawnPoint7;
    public Transform spawnPoint8;

    public GameObject AI;

    private int count = 0;
    private int limit = 10;
    private int random;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        count++;

        if (count > limit)
        {
            count = 0;

            random = Random.Range(0, 10);

            switch (random)
            {
                case (1):
                    GameObject plane1 = (GameObject)Instantiate(AI,spawnPoint1.position,Quaternion.identity);
                    plane1.GetComponent<AgentPlaneScript>().chooseTarget = Random.Range(1, 4);
                    break;

                case (2):
                    GameObject plane2 = (GameObject)Instantiate(AI,spawnPoint2.position,Quaternion.identity);
                    plane2.GetComponent<AgentPlaneScript>().chooseTarget = Random.Range(1, 4);
                    break;

                case (3):
                    int[] ran1 = {0,1,3};
                    GameObject plane3 = (GameObject)Instantiate(AI,spawnPoint3.position,Quaternion.identity);
                    plane3.GetComponent<AgentPlaneScript>().chooseTarget = ran1[Random.Range(0, 3)];
                    break;

                case (4):
                    int[] ran2 = { 0, 1, 3 };
                    GameObject plane4 = (GameObject)Instantiate(AI,spawnPoint4.position,Quaternion.identity);
                    plane4.GetComponent<AgentPlaneScript>().chooseTarget = ran2[Random.Range(0, 3)];
                    break;

                case (5):
                    GameObject plane5 = (GameObject)Instantiate(AI,spawnPoint5.position,Quaternion.identity);
                    plane5.GetComponent<AgentPlaneScript>().chooseTarget = Random.Range(0, 3);
                    break;

                case (6):
                    GameObject plane6 = (GameObject)Instantiate(AI,spawnPoint6.position,Quaternion.identity);
                    plane6.GetComponent<AgentPlaneScript>().chooseTarget = Random.Range(0, 3);
                    break;

                case (7):
                    int[] ran3 = { 0, 2, 3 };
                    GameObject plane7 = (GameObject)Instantiate(AI,spawnPoint7.position,Quaternion.identity);
                    plane7.GetComponent<AgentPlaneScript>().chooseTarget = ran3[Random.Range(0, 3)];
                    break;

                case (8):
                    int[] ran4 = { 0, 2, 3 };
                    GameObject plane8 = (GameObject)Instantiate(AI,spawnPoint8.position,Quaternion.identity);
                    plane8.GetComponent<AgentPlaneScript>().chooseTarget = ran4[Random.Range(0, 3)];
                    break;
            }
        }
    }
}

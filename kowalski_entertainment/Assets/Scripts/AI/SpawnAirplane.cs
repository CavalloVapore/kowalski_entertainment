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
                    Instantiate(AI, spawnPoint1.position, Quaternion.identity);
                    break;

                case (2):
                    Instantiate(AI, spawnPoint2.position, Quaternion.identity);
                    break;

                case (3):
                    Instantiate(AI, spawnPoint3.position, Quaternion.identity);
                    break;

                case (4):
                    Instantiate(AI, spawnPoint4.position, Quaternion.identity);
                    break;

                case (5):
                    Instantiate(AI, spawnPoint5.position, Quaternion.identity);
                    break;

                case (6):
                    Instantiate(AI, spawnPoint6.position, Quaternion.identity);
                    break;

                case (7):
                    Instantiate(AI, spawnPoint7.position, Quaternion.identity);
                    break;

                case (8):
                    Instantiate(AI, spawnPoint8.position, Quaternion.identity);
                    break;
            }
        }
    }
}

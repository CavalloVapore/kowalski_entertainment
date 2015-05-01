using UnityEngine;
using System.Collections;

public class KillAI : MonoBehaviour 
{

	void OnTriggerEnter( Collider other)
    {
        Destroy(other.gameObject);
    }
}

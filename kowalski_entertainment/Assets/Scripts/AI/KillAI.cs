using UnityEngine;
using System.Collections;

public class KillAI : MonoBehaviour 
{

	void OnTriggerEnter( Collider other)
    {
        Network.RemoveRPCs(other.gameObject.GetComponent<NetworkView>().viewID);
        Network.Destroy(other.gameObject);
    }
}

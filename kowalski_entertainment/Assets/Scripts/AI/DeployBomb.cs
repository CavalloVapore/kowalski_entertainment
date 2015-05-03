using UnityEngine;
using System.Collections;

public class DeployBomb : MonoBehaviour {

    public Detonator bomb;

    void OnTriggerEnter(Collider other)
    {
        bomb.Explode();
    }
}

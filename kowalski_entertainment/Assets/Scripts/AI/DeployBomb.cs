using UnityEngine;
using System.Collections;

public class DeployBomb : MonoBehaviour {

    public Detonator bomb;

    void OnTriggerEnter(Collider other)
    {
        Invoke("TriggerExplosion", 1.0f);
    }

    void TriggerExplosion()
    {
        bomb.Explode();
    }
}

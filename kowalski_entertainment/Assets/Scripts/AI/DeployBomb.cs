using UnityEngine;
using System.Collections;

public class DeployBomb : MonoBehaviour {

    public Detonator bomb;
    public AudioClip explosionSFX;

    void OnTriggerEnter(Collider other)
    {
        Invoke("TriggerExplosion", 1.0f);
    }

    void TriggerExplosion()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        bomb.Explode();
    }
}

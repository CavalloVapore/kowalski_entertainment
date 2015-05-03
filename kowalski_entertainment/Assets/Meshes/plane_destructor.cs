using UnityEngine;
using System.Collections;

public class plane_destructor : MonoBehaviour 
{
    public float wreck_lifetime = 6.0f;
    public AudioClip explosionSFX;

	public void Explode()
    {

        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        // Get Detonator, there's only one so this works
        Detonator exp = transform.GetComponentInChildren<Detonator>();
        // Save healthy GO
        MeshRenderer healthy_il2 = transform.Find("Bone01/IL-2_Body").GetComponent<MeshRenderer>();
        healthy_il2.enabled = false;

        // Get wreckages
        Transform wreckage1 = transform.Find("IL2_wreck");
        Transform wreckage2 = transform.Find("IL2_wreck2");

        MeshRenderer w1 = wreckage1.GetComponent<MeshRenderer>();
        MeshRenderer w2 = wreckage2.GetComponent<MeshRenderer>();

        Rigidbody rb1 = wreckage1.GetComponent<Rigidbody>();
        Rigidbody rb2 = wreckage2.GetComponent<Rigidbody>();

        w1.enabled = true;
        w2.enabled = true;
        rb1.isKinematic = false;
        rb2.isKinematic = false;
        rb1.detectCollisions = true;
        rb2.detectCollisions = true;
        rb1.AddExplosionForce(15000.0f, new Vector3(rb1.position.x - 3, rb1.position.y + 3, rb1.position.z), 5.0f);
        rb2.AddExplosionForce(8000.0f, new Vector3(rb2.position.x - 3, rb2.position.y + 3, rb2.position.z), 5.0f);
        exp.Explode();
        Invoke("CleanUp", wreck_lifetime);
	}

    void CleanUp()
    {
        Network.Destroy(gameObject);
        //Destroy(gameObject);
    }
}

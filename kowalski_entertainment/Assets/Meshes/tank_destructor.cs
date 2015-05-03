using UnityEngine;
using System.Collections;

public class tank_destructor : MonoBehaviour 
{
    public float wreck_lifetime = 8.0f;
    public AudioClip explosionSFX;

	public void Explode()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position);
        // Get Detonator, there's only one so this works
        Detonator exp = transform.GetComponentInChildren<Detonator>();
        // Save healthy GO
        MeshRenderer healthy_jeep = transform.Find("MainHull").GetComponent<MeshRenderer>();
        healthy_jeep.enabled = false;
        MeshRenderer healthy_jeep1 = transform.Find("Bone01/wheel01").GetComponent<MeshRenderer>();
        healthy_jeep1.enabled = false;
        MeshRenderer healthy_jeep2 = transform.Find("Bone02/wheel02").GetComponent<MeshRenderer>();
        healthy_jeep2.enabled = false;
        MeshRenderer healthy_jeep3 = transform.Find("Bone03/wheel03").GetComponent<MeshRenderer>();
        healthy_jeep3.enabled = false;
        MeshRenderer healthy_jeep4 = transform.Find("Bone04/wheel04").GetComponent<MeshRenderer>();
        healthy_jeep4.enabled = false;
        MeshRenderer healthy_jeep5 = transform.Find("Bone05/wheel05").GetComponent<MeshRenderer>();
        healthy_jeep5.enabled = false;
        MeshRenderer healthy_jeep6 = transform.Find("Bone06/wheel06").GetComponent<MeshRenderer>();
        healthy_jeep6.enabled = false;
        MeshRenderer healthy_jeep7 = transform.Find("Bone07/wheel07").GetComponent<MeshRenderer>();
        healthy_jeep7.enabled = false;
        MeshRenderer healthy_jeep8 = transform.Find("su_tread_r").GetComponent<MeshRenderer>();
        healthy_jeep8.enabled = false;

        // Get main wreckage
        Transform WRECK_main = transform.Find("WRECK");
        MeshRenderer w1 = WRECK_main.GetComponent<MeshRenderer>();
        Rigidbody rb1 = WRECK_main.GetComponent<Rigidbody>();
        w1.enabled = true;
        rb1.isKinematic = false;
        rb1.detectCollisions = true;

        // Process debris
        Transform w_fly1 = transform.Find("wreck_fly1");
        Transform w_fly2 = transform.Find("wreck_fly2");
        Transform w_fly3 = transform.Find("wreck_fly3");

        MeshRenderer mr_fly1 = w_fly1.GetComponent<MeshRenderer>();
        MeshRenderer mr_fly2 = w_fly2.GetComponent<MeshRenderer>();
        MeshRenderer mr_fly3 = w_fly3.GetComponent<MeshRenderer>();

        Rigidbody rb_fly1 = w_fly1.GetComponent<Rigidbody>();
        Rigidbody rb_fly2 = w_fly2.GetComponent<Rigidbody>();
        Rigidbody rb_fly3 = w_fly3.GetComponent<Rigidbody>();

        mr_fly1.enabled = true;
        mr_fly2.enabled = true;
        mr_fly3.enabled = true;

        rb_fly1.isKinematic = false;
        rb_fly2.isKinematic = false;
        rb_fly3.isKinematic = false;

        rb_fly1.detectCollisions = true;
        rb_fly2.detectCollisions = true;
        rb_fly3.detectCollisions = true;

        rb_fly1.AddExplosionForce(100.0f, rb_fly1.position, 5.0f);
        rb_fly2.AddExplosionForce(100.0f, rb_fly2.position, 5.0f);
        rb_fly3.AddExplosionForce(100.0f, rb_fly3.position, 5.0f);

        exp.Explode();
        Invoke("CleanUp", wreck_lifetime);
	}

    void CleanUp()
    {
        Network.Destroy(gameObject);
        //Destroy(gameObject);
    }
}

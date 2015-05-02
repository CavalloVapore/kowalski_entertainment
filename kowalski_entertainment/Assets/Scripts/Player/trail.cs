using UnityEngine;
using System.Collections;

public class trail : MonoBehaviour {

    private float visibility;
    private LineRenderer lr;
	// Use this for initialization
	void Start () {
        visibility = 0.5f;
        lr = GetComponent<LineRenderer>();
        lr.material.SetFloat("_InvFade", 3);
	}
	
	// Update is called once per frame
	void Update () {
	    visibility -= Time.deltaTime;
        if(visibility < 0) 
            Destroy(gameObject);
        lr.material.SetColor("_TintColor", new Color(.5f, .5f, .5f, visibility));
	}
}

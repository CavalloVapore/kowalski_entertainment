using UnityEngine;
using System.Collections;

public class AddCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInParent<Canvas>().worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class tank_thread_texture : MonoBehaviour 
{

    public MeshRenderer r_tracks;
    public float speed_of_tracks = 50.0f;

	// Use this for initialization
	void Start () 
    {
        r_tracks = transform.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        float offset = Time.time * speed_of_tracks;
        r_tracks.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
	}
}

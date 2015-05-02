using UnityEngine;
using System.Collections;

public class hatch_rot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public bool open_hatch = false;

    public void Update()
    {
        Vector3 to = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 290);
        Vector3 from = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180);

        if ((Vector3.Distance(transform.eulerAngles, to) > 0.02f) && (open_hatch))
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
        }
        else if ((Vector3.Distance(transform.eulerAngles, from) > 0.02f) && (!open_hatch))
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, from, Time.deltaTime);
        }
        else if (transform.eulerAngles == to)
        {
                transform.eulerAngles = to;
        }
        else if (transform.eulerAngles == from)
        {
            transform.eulerAngles = from;
        }
   }
}

using UnityEngine;
using System.Collections;

public class SpinThatShit : MonoBehaviour {

    public enum chooseRotation
    {
        x,
        y,
        z
    }
    public chooseRotation myRot;
    public float speed;

	// Use this for initialization
	void Start () {
        myRot = chooseRotation.x;
	}
	
	// Update is called once per frame
	void Update () {
        switch (myRot)
        {
            case chooseRotation.x:
            {
                this.transform.Rotate(new Vector3(speed, 0, 0)*Time.deltaTime);
            }
            break;
            case chooseRotation.y:
            {
                this.transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
            }
            break;
            case chooseRotation.z:
            {
                this.transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
            }
            break;
        }
	}
}

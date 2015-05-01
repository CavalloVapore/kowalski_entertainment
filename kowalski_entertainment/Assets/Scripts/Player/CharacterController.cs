using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    enum Position { NORTH, EAST, SOUTH, WEST };

    private Position myPosition;
    private bool overheat;
    private float heat;
    private float heatInc;
    private float heatDec;

    private Transform posNorth;
    private Transform posEast;
    private Transform posSouth;
    private Transform posWest;

    // Use this for initialization
	void Start () 
    {
        myPosition = Position.NORTH;
        heat = 0;
        heatInc = 10;
        heatDec = 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //Input

        //Feuern
        if (Input.GetButtonDown("Fire1") && !overheat)
        {
            Fire();
        }

        //Postionen Switchen
        //Switch Rechts
        /* if (Input.GetButtonDown("SwitchRechts"))
         {
             myPosition = (Position)(((int)myPosition + 1) % 4);
             Switch();
         }
         //Switch Links
         if (Input.GetButtonDown("SwitchLinks"))
         {
             myPosition = (Position)(((int)myPosition - 1) % 4);
             Switch();
         }
        */
    }

    void FixedUpdate()
    {
        if (heat == 100)
        {
            overheat = true;
        }
        
        if (heat > 0)
        {
            heat = Mathf.Max((heat - heatDec), 0);
        }
        Debug.Log(heat);

        if (heat == 0)
        {
            overheat = false;
        }
    }


    void Fire()
    {
      
        //Fire

        //Heat
        heat = Mathf.Min((heat + heatInc), 100);
    }

    void Switch()
    {

    }
}

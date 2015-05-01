using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    enum Position { NORTH, EAST, SOUTH, WEST };

    private Position myPosition;
    private bool overheat;
    private float heat;
    private float heatInc;
    private float heatDec;
    private int money;

    private Transform posNorth;
    private Transform posEast;
    private Transform posSouth;
    private Transform posWest;

    public GameObject projectilePrefab;
    private RaycastHit hit;
    private Ray ray;

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
        //Ray
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
	    //Input

        //Shop?
        
        //Feuern
        if (Input.GetButtonDown("Fire1") && !overheat)
        {
            Fire();
        }

        //Postionen Switchen
        //Switch Rechts
         if (Input.GetButtonDown("SwitchRechts"))
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
        switch (myPosition)
        {
            case Position.NORTH:
                break;
            case Position.EAST:
                break;
            case Position.SOUTH:
                break;
            case Position.WEST:
                break;
        }
    }
}

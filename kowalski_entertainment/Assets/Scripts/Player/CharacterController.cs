using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class CharacterController : MonoBehaviour
{

    enum Position { NORTH, EAST, SOUTH, WEST };

    private Position myPosition;
    private bool overheat;
    private float heat;
    private float heatInc;
    private float heatDec;

    public Transform posNorth;
    public Transform posEast;
    public Transform posSouth;
    public Transform posWest;

    public SimpleMouseRotator smr; 

    public GameObject projectilePrefab;


    // Use this for initialization
    void Start()
    {
        myPosition = Position.NORTH;
        Switch();
        heat = 0;
        heatInc = 10;
        heatDec = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        //Ray
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
        //RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        // if(Physics.Raycast(ray, hit, 100f, ))
        //  {

        //  }

        //Input


        //Feuern
        if (Input.GetButtonDown("Fire1") && !overheat)
        {
            Fire();
        }

    }

    void FixedUpdate()
    {
        //Überhitzen
        if (heat == 100)
        {
            overheat = true;
        }

        //Hitzeverlust über Zeit
        if (heat > 0)
        {
            heat = Mathf.Max((heat - heatDec), 0);
        }
        //Debug.Log(heat);

        //Ende Überhitzung
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
                transform.position = posNorth.position;
                smr.m_OriginalRotation = posNorth.rotation;
                break;
            case Position.EAST:
                transform.position = posEast.position;
                smr.m_OriginalRotation = posEast.rotation;
                break;
            case Position.SOUTH:
                transform.position = posSouth.position;
                smr.m_OriginalRotation = posSouth.rotation;
                break;
            case Position.WEST:
                transform.position = posWest.position;
                smr.m_OriginalRotation = posWest.rotation;
                break;
        }
    }

    public void setPosition(int position)
    {
        myPosition = (Position)(position);
        Switch();
    }
}

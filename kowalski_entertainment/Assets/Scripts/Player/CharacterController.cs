using UnityEngine;
using UnityEngine.UI;
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

    public Texture2D crosshair;

    public Transform posNorth;
    public SimpleMouseRotator smr;

    public GameObject projectilePrefab;
    public GameObject tracer;
    public float spreadFactor;
    private float spread;
    private float spreadInc;
    private float spreadDec;

    public Image bar;

    private int damage;
    private float atkSpeed;
    private float lastShot;

    private LineRenderer lr;

    private Vector3 hitPos;
    Ray ray;
    RaycastHit hit;

    public NetworkView nView;

    // Use this for initialization
    void Start()
    {
        bar = GetComponentInChildren<Image>();

        heat = 0;
        heatInc = 10;
        heatDec = 0.5f;
        spread = 1f;
        spreadInc = 0.1f;
        spreadDec = 0.1f;
        atkSpeed = 0.2f;
        damage = 2;
        lr = GetComponent<LineRenderer>();

        posNorth = GameObject.FindGameObjectWithTag("North").transform;

        transform.position = posNorth.position;
        smr.m_OriginalRotation = posNorth.rotation;

        if (nView.isMine)
        {
            Camera.main.transform.position = this.transform.position;
            Camera.main.transform.rotation = this.transform.rotation;
            Camera.main.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, 17.0f, transform.position.z);
        if(transform.position.x < -8)
            transform.position = new Vector3(-8, transform.position.y, transform.position.z);
        if (transform.position.x > 22)
            transform.position = new Vector3(22, transform.position.y, transform.position.z);
        if (transform.position.z > 16)
            transform.position = new Vector3(transform.position.x, transform.position.y, 16);
        if (transform.position.z < -12)
            transform.position = new Vector3(transform.position.x, transform.position.y, -12);

        //ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height, 0f));

        //Input


        //Feuern
        if (Input.GetButton("Fire1") && !overheat && Time.time > (lastShot + atkSpeed))
        {
            //Debug.Log("RATATA");
            //Debug.Log(lastShot);
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f + Random.Range(-spreadFactor * spread, spreadFactor * spread), Screen.height * 0.5f + Random.Range(-spreadFactor * spread, spreadFactor * spread), 0f));
            Fire();
        }
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        bar.fillAmount = heat / 100;

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


        //Spreadverlust
        if (spread > 0 && !Input.GetButton("Fire1"))
        {
            spread = Mathf.Max((spread - spreadDec), 0);
        }
        //Debug.Log(spread);

    }


    void Fire()
    {
        
        
        //Fire
        if (Physics.Raycast(ray, out hit))
        {
            hitPos = hit.point;
            //Enemyhit
            if (hit.collider.gameObject.tag == "Enemy")
            {
                //ENEMY LOSE LIFE
                hit.collider.gameObject.GetComponent<EnemyUnit>().ReduceHealth(damage);
            }
            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            hitPos = ray.origin + ray.direction * 50;
        }

        GameObject temp = (GameObject)Instantiate(tracer);
        temp.GetComponent<LineRenderer>().SetPosition(0, ray.origin + ray.direction * 2);
        temp.GetComponent<LineRenderer>().SetPosition(1, hitPos);
        //Heat
        heat = Mathf.Min((heat + heatInc), 100);
        //Spread
        spread = Mathf.Min((spread + spreadInc), 30);

        //Last Shot
        lastShot = Time.time;
    }
     void OnGUI()
     {
         float x = (Screen.width/2) - (crosshair.width/2);
         float y = (Screen.height/2) - (crosshair.height/2);
         GUI.DrawTexture(new Rect(x, y, crosshair.width, crosshair.height), crosshair);
     }
     
}

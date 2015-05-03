using UnityEngine;
using System.Collections;

public class Bunker : MonoBehaviour {

    public int health;
    private NetworkView nView;
    private bool occ1 = false;
    private bool occ2 = false;
    private bool occ3 = false;
    private bool occ4 = false;
    private bool occ5 = false;
    private bool occ6 = false;
    private bool occ7 = false;
    private bool occ8 = false;
    private bool occ9 = false;
    private bool occ10 = false;
    private bool occ11 = false;
    private bool occ12 = false;
    private bool occ13 = false;
    private bool occ14 = false;
    private bool occ15 = false;
    private bool occ16 = false;

    public Transform pos1;//n
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;//s
    public Transform pos6;
    public Transform pos7;
    public Transform pos8;
    public Transform pos9;//e
    public Transform pos10;
    public Transform pos11;
    public Transform pos12;
    public Transform pos13;//w
    public Transform pos14;
    public Transform pos15;
    public Transform pos16;

    public GameObject test;

	// Use this for initialization
	void Start () {
        nView = GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K))
            Debug.Log(occ1);
            
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref occ1);
        }
        else
        {
            stream.Serialize(ref occ1);
        }
    }
}

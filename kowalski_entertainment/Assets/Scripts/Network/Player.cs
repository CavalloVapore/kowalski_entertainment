using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float rotSpeed = 10f;

    public int player;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;
    private Vector3 syncStartRotation = Vector3.zero;
    private Vector3 syncEndRotation = Vector3.zero;
    private NetworkView nView;

    public string text;

    private bool isHatchOpen = false;

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncRotation = Vector3.zero;
        if (stream.isWriting)
        {
            syncPosition = transform.position;
            stream.Serialize(ref syncPosition);

            syncRotation = transform.localEulerAngles;
            stream.Serialize(ref syncRotation);
        }
        else
        {
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncRotation);

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = syncPosition + Vector3.one * syncDelay;
            syncStartPosition = transform.position;

            syncEndRotation = syncRotation + Vector3.one * syncDelay;
            syncStartRotation = transform.localEulerAngles;
        }
    }

    void Awake()
    {
        lastSynchronizationTime = Time.time;
        nView = GetComponent<NetworkView>();
        ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    void Update()
    {
        if (nView.isMine)
        {
            InputMovement();
            InputText();
            InputColorChange();
        }
        else
        {
            SyncedMovement();
        }
    }


    private void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotSpeed, Space.World);

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotSpeed, Space.World);
    }

    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;

        this.transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        this.transform.localEulerAngles = Vector3.Lerp(syncStartRotation, syncEndRotation, syncTime / syncDelay);
    }

    public void InputColorChange()
    {
        if (Input.GetKey(KeyCode.R))
        ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    [RPC] void ChangeColorTo(Vector3 color)
    {
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);

        if (nView.isMine)
            nView.RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
    }

    public void InputText()
    {
        if (Input.GetButtonDown("G"))
            Chat("Player " + nView.viewID + ": Hi!");
    }

    [RPC]
    void Chat(string a)
    {
        text = a;
        if (nView.isMine)
            nView.RPC("Chat", RPCMode.OthersBuffered, a);
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 500, 100), text);
    }



}

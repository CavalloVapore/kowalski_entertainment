using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncVelocity = Vector3.zero;
        if (stream.isWriting)
        {
            syncPosition = transform.position;
            stream.Serialize(ref syncPosition);

            //syncPosition = transform.velocity;
            //stream.Serialize(ref syncVelocity);
        }
        else
        {
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncVelocity);

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = transform.position;
        }
    }

    void Awake()
    {
        lastSynchronizationTime = Time.time;
    }

    void Update()
    {
        if (GetComponent<NetworkView>().isMine)
        {
            InputMovement();
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
            transform.Translate(Vector3.forward);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left);
    }

    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;

        this.transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }


    private void InputColorChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ChangeColorTo(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    [RPC] void ChangeColorTo(Vector3 color)
    {
        GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z, 1f);

        if (GetComponent<NetworkView>().isMine)
            GetComponent<NetworkView>().RPC("ChangeColorTo", RPCMode.OthersBuffered, color);
    }
}

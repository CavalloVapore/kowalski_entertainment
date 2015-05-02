using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour 
{ 
    public EnemyType enemyType;
    private int enemyHealth;
    public int maxHealth;

    public NetworkView nView;

    public enum EnemyType
    {
        Vehicle,
        Tank,
        Plane
    }

	// Use this for initialization
	void Start () 
    {
        enemyHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enemyHealth <= 0)
        {
            //Debug.Log(enemyUnit.enemyHealth);
            Debug.Log(nView.owner);
            Network.RemoveRPCs(nView.viewID);
            Network.Destroy(gameObject);
        }
	}

    public void ReduceHealth (int damage)
    {
        enemyHealth -= 1;
        //syncHealth(enemyHealth);
    }

    /*[RPC]
    public void syncHealth(int health)
    {
        nView.RPC("syncHealth", RPCMode.OthersBuffered, enemyHealth);
    }*/

    /*void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        int syncHealth = enemyHealth;
        if (stream.isWriting)
        {
            syncHealth = enemyHealth;
            stream.Serialize(ref syncHealth);
        }
        else
        {
            stream.Serialize(ref syncHealth);
            enemyHealth = syncHealth;
        }
    }*/

    public int getHealth()
    {
        return enemyHealth;
    }
}

using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour 
{ 
    public EnemyType enemyType;
    private int enemyHealth;
    public int maxHealth;

    public NetworkView nView;

    bool isExploded = false;

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
        if (enemyHealth <= 0 && isExploded == false)
        {
            //Debug.Log(enemyUnit.enemyHealth);
            isExploded = true;
            Network.RemoveRPCs(nView.viewID);
            switch(gameObject.name)
            { 
                case("EnemyLV1(Clone)"):
                    GetComponent<jeep_destructor>().Explode();
                    break;

                case ("EnemyLV2(Clone)"):
                    GetComponent<armoured_jeep_destructor>().Explode();
                    break;
            }
            GetComponent<NavMeshAgent>().Stop();
        }
	}

    public void ReduceHealth (int damage)
    {
        enemyHealth -= damage;
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

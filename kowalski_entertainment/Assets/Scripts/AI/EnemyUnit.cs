using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour 
{ 
    public EnemyType enemyType;
    private int enemyHealth;
    public int maxHealth;

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
            Network.Destroy(gameObject);
        }
	}

    public void ReduceHealth (int damage)
    {
        enemyHealth -= damage;
    }

    public int getHealth()
    {
        return enemyHealth;
    }
}

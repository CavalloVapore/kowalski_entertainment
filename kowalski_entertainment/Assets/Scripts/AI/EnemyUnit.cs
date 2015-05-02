using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour 
{
    private Enemy enemyUnit = new Enemy();
    private string saveName;
    private EnemyDatabase database;

	// Use this for initialization
	void Start () 
    {
        database = GameObject.FindGameObjectWithTag("Enemy Database").GetComponent<EnemyDatabase>();
        saveName = gameObject.name;

	    switch(saveName)
        {
            case("EnemyLV1"):
                enemyUnit = database.enemy[0];
                break;

            case ("EnemyLV2"):
                enemyUnit = database.enemy[1];
                break;

            case ("EnemyLV3"):
                enemyUnit = database.enemy[2];
                break;

            case ("EnemyLV4"):
                enemyUnit = database.enemy[3];
                break;

            case ("EnemyLV5"):
                enemyUnit = database.enemy[4];
                break;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enemyUnit.enemyHealth <= 0)
        {
            Network.Destroy(gameObject);
        }
	}

    public void ReduceHealth (int damage)
    {
        enemyUnit.enemyHealth -= damage;
    }
}

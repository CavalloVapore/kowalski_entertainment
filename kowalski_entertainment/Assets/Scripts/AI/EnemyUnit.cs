using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour 
{
    public Enemy enemyUnit = new Enemy();
    private string saveName;
    private EnemyDatabase database;

	// Use this for initialization
	void Start () 
    {
        database = GameObject.FindGameObjectWithTag("Enemy Database").GetComponent<EnemyDatabase>();
        saveName = gameObject.name;

        //Debug.Log(saveName);

	    switch(saveName)
        {
            case("EnemyLV1(Clone)"):
                enemyUnit = database.enemy[0];
                break;

            case ("EnemyLV2(Clone)"):
                enemyUnit = database.enemy[1];
                break;

            case ("EnemyLV3(Clone)"):
                enemyUnit = database.enemy[2];
                break;

            case ("EnemyLV4(Clone)"):
                enemyUnit = database.enemy[3];
                break;

            case ("EnemyLV5(Clone)"):
                enemyUnit = database.enemy[4];
                break;
        }

        //Debug.Log(enemyUnit.enemyID);
        //Debug.Log(enemyUnit.enemyHealth);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enemyUnit.enemyHealth <= 0)
        {
            //Debug.Log(enemyUnit.enemyHealth);
            Network.Destroy(gameObject);
        }
	}

    public void ReduceHealth (int damage)
    {
        this.enemyUnit.enemyHealth -= damage;
    }
}

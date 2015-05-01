using UnityEngine;
using System.Collections;




[System.Serializable]
public class Enemy
{
    
    public string enemySave;         
    public int enemyID;             
    public string enemyTitle;       
    public GameObject enemyObject;  
    public EnemyType enemyType;
    public int enemyHealth;
    public int enemySpeed;
    public int enemyPriority;

    
    public enum EnemyType
    {
        Vehicle,
        Tank,
        Plane
    }

    
    public Enemy(string save, int id, string title, EnemyType type, int health, int speed, int priority)
    {
        enemySave = save;
        enemyID = id;
        enemyTitle = title;
        enemyType = type;
        enemyHealth = health;
        enemySpeed = speed;
        enemyPriority = priority;
    }

   
    public Enemy()
    {
        enemyID = -1;
    }

    //loads the high res image -- call only if there exists an high res image
    public void LoadGameObject()
    {
        enemyObject = Resources.Load<GameObject>("EnemyObject/" + enemySave);
    }
}

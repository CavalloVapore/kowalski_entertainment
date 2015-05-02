using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Database for the items that can be found in the inventory



public class EnemyDatabase : MonoBehaviour
{
    //creates List as a database for all items
    public List<Enemy> enemy = new List<Enemy>();

    //Adding the different Items to the List of all Items
    void Start()
    {
        enemy.Add(new Enemy("EnemyLV1",0,"Jeep",Enemy.EnemyType.Vehicle,4,10,5,80,3,10));
        enemy.Add(new Enemy("EnemyLV2", 1, "Truck", Enemy.EnemyType.Vehicle, 150, 7, 3,40,7,4));
        enemy.Add(new Enemy("EnemyLV3", 2, "lightly armored Vehicle", Enemy.EnemyType.Vehicle, 300, 5, 4,60,5,7));
        enemy.Add(new Enemy("EnemyLV4", 3, "Tank", Enemy.EnemyType.Tank, 1000, 2, 2,20,3,2));
        enemy.Add(new Enemy("EnemyLV5", 4, "Plane", Enemy.EnemyType.Tank, 100, 20, 1,120,20,20));

        for(int i = 0; i < enemy.Count; i++)
        {
            enemy[i].LoadGameObject();
        }
    }
}

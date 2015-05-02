using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitsUI : MonoBehaviour {

    private Transform target;
    private GameObject mainCamera;
    private Slider healthbar;
    private Enemy enemyUnit = new Enemy();

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        enemyUnit = gameObject.GetComponentInParent<EnemyUnit>().enemyUnit;
        healthbar = gameObject.GetComponentInChildren<Slider>();
        healthbar.maxValue = enemyUnit.enemyHealth;
        healthbar.value = enemyUnit.enemyHealth;
        if (mainCamera)
            target = mainCamera.transform;
        else
            Debug.LogError("Can´t find MainCamera!", this);
    }

    void LateUpdate()
    {
        transform.LookAt(target);
        healthbar.value = enemyUnit.enemyHealth;
    }    
}

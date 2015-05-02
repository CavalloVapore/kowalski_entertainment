using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitsUI : MonoBehaviour {

    private Transform target;
    private GameObject mainCamera;
    private Slider healthbar;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        healthbar = gameObject.GetComponentInChildren<Slider>();
        healthbar.maxValue = gameObject.GetComponentInParent<EnemyUnit>().maxHealth;
        healthbar.value = gameObject.GetComponentInParent<EnemyUnit>().getHealth();
        if (mainCamera)
            target = mainCamera.transform;
        else
            Debug.LogError("Can´t find MainCamera!", this);
    }

    void LateUpdate()
    {
        transform.LookAt(target);
        healthbar.value = gameObject.GetComponentInParent<EnemyUnit>().getHealth();
    }    
}

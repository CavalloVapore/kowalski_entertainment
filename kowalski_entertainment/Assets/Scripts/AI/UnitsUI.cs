using UnityEngine;
using System.Collections;

public class UnitsUI : MonoBehaviour {

    private Transform target;
    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera)
            target = mainCamera.transform;
        else
            Debug.LogError("Can´t find MainCamera!", this);
    }

    void LateUpdate()
    {
        //TODO: try to do it only if needed
        transform.LookAt(target);
    }    
}

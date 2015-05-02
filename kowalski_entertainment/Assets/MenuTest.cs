using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuTest : MonoBehaviour {

    public GameObject startButton;
    public GameObject joinButton;

	// Use this for initialization
    void Start()
    {
        startButton.GetComponent<Button>().onClick.RemoveAllListeners();
        joinButton.GetComponent<Button>().onClick.RemoveAllListeners();
        startButton.GetComponent<Button>().onClick.AddListener(() => { yo(); });
        joinButton.GetComponent<Button>().onClick.AddListener(() => { yu(); });
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void yo()
    {
        Debug.Log("Yo");
    }
    public void yu()
    {
        Debug.Log("Yu");
    }
}

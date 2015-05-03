using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviour
{
    private const string typeName = "TestGame";
    private const string gameName = "TestRoom";

    private bool isRefreshingHostList = false;
    private HostData[] hostList;

    public GameObject playerPrefab;

    public Transform spawn1;

    private int playerCount = 1;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public Transform spawnPoint7;
    public Transform spawnPoint8;

    public GameObject startButton;
    public GameObject joinButton;
    public Canvas canvas;

    public GameObject AI;

    private int count = 0;
    private int random;


    void Start()
    {
        //if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
        startButton.GetComponent<Button>().onClick.AddListener(() => { StartServer(); });

        //if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
        joinButton.GetComponent<Button>().onClick.AddListener(() => { RefreshHostList(); });
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    private void StartServer()
    {
        Network.InitializeServer(5, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
        canvas.enabled = false;
    }

    void OnServerInitialized()
    {
        SpawnPlayer();
    }


    void Update()
    {
        if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
        {
            isRefreshingHostList = false;
            hostList = MasterServer.PollHostList();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i <= 5; i++)
            {
                random = Random.Range(0, 8);

                switch (random)
                {
                    case (1):
                        Network.Instantiate(AI, spawnPoint1.position, Quaternion.identity, 0);
                        break;

                    case (2):
                        Network.Instantiate(AI, spawnPoint2.position, Quaternion.identity, 0);
                        break;

                    case (3):
                        Network.Instantiate(AI, spawnPoint3.position, Quaternion.identity, 0);
                        break;

                    case (4):
                        Network.Instantiate(AI, spawnPoint4.position, Quaternion.identity, 0);
                        break;

                    case (5):
                        Network.Instantiate(AI, spawnPoint5.position, Quaternion.identity, 0);
                        break;

                    case (6):
                        Network.Instantiate(AI, spawnPoint6.position, Quaternion.identity, 0);
                        break;

                    case (7):
                        Network.Instantiate(AI, spawnPoint7.position, Quaternion.identity, 0);
                        break;

                    case (8):
                        Network.Instantiate(AI, spawnPoint8.position, Quaternion.identity, 0);
                        break;
                }
            }
        }
    }

    private void RefreshHostList()
    {
        if (!isRefreshingHostList)
        {
            isRefreshingHostList = true;
            MasterServer.RequestHostList(typeName);
        }
    }


    private void JoinServer(HostData hostData)
    {
        canvas.enabled = false;
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, spawn1.position, Quaternion.identity, 0);
    }

    private void OnPlayerConnected(NetworkPlayer player)
    {
        playerCount++;
        Debug.Log("Player " + playerCount + " connected from " + player.ipAddress + ":" + player.port);
    }
}

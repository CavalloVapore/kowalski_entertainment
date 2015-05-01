using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class Host : MonoBehaviour
{

    string gameName = "TestName";
    bool refreshing = false;

    public Host host;
    public HostData[] hostData;
    GameObject playerPrefab;

    bool create = false, joining = false;
    string serverInfo = "";
    string serverName = "";
    string serverPass = "";
    string playerName = "";
    string clientPass = "";
    Vector2 scrollPosition = new Vector2();

    // Use this for initialization
    void Start()
    {
        playerName = PlayerPrefs.GetString("Player Name");
    }

    // Update is called once per frame
    void Update()
    {
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {
                refreshing = false;
                hostData = MasterServer.PollHostList();
            }
        }
    }

    void OnGui(){
        if(!Network.isClient && !Network.isServer) { // if you arent the server or a client
            if(!create && !joining){
                if (GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2,100,20),"Create Game")) {
                    create = true;
                }
                if (GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 + 30,100,20),"Find Game")) {
                joining = true;
                refreshHostList();
                }
            } 
        }
        if (create){
            if (GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/3 + 110,100,50),"Create")) {
                    startServer();
            }
   
            GUI.Label(new Rect (Screen.width/2 - 110,Screen.height/3,100,20),"Server Name:");
            GUI.Label(new Rect (Screen.width/2 + 40,Screen.height/3,100,20),"Password:");
            GUI.Label(new Rect (Screen.width/2 - 30,Screen.height/2 + 90,100,20),"Server Info:");
    
            serverName = GUI.TextField (new Rect (Screen.width/2 - 120,Screen.height/3 + 30,100,20), serverName, 12);
            serverPass = GUI.PasswordField (new Rect (Screen.width/2 + 20,Screen.height/3 + 30,100,20), serverPass, "*"[0], 12);
            serverInfo = GUI.TextArea (new Rect (Screen.width/2 - 70,Screen.height/2 + 120,150,40), serverInfo, 35);
   
            if (GUI.Button(Rect(Screen.width/1.2,Screen.height/20,100,20),"Back")) {
                create = false;
            }
        }
  
        if (joining){
  
            if(hostData) {
                GUI.BeginScrollView (new Rect((float)(Screen.width/4),(float)(Screen.height/6),(float)(Screen.width/1.5),(float)(Screen.height/2)),scrollPosition, new Rect (0, 0, 300, 1000/hostData.Length * 30/);
    
                GUI.Label(new Rect(30,0,100,20),"Game Name");
                GUI.Label(new Rect(350,0,100,20),"Server Info");
                GUI.Label(new Rect(590,0,100,20),"Player Count");
                GUI.Label(new Rect(700,0,100,20),"Password");
    
                for(int i= 0; i < hostData.Length; i++) {
           
                   GUI.Label(new Rect(0,30 + i * 30,200,22),hostData[i].gameName);
                   GUI.Label(new Rect(160,30 + i * 30,500,22),hostData[i].comment);
                   GUI.Label(new Rect(610,30 + i * 30,100,20),hostData[i].connectedPlayers + " / " + hostData[i].playerLimit);

                   if (hostData[i].passwordProtected){
           
                    clientPass = GUI.PasswordField(new Rect (680,30 + i * 30,100,25), clientPass, "*"[0], 12);
                   }
           
                   if (GUI.Button(new Rect(800,30 + i * 30,100,25),"Join")) {
      
                    Network.Connect(hostData[i], clientPass);
                   }
               }    
               GUI.EndScrollView ();
           }
         
           if(!hostData){
               GUI.Label(new Rect(Screen.width/2 - 50,Screen.height/3,200,25),"No Games Found");
               if (GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/3 + 30,105,25),"Refresh")) {
    
           refreshHostList();
           }
       }
   
       if (GUI.Button(new Rect((float)(Screen.width/1.2),(float)(Screen.height/20),100,20),"Back")) {
              joining = false;
       }
    }
    if (GUI.Button(new Rect(Screen.width/20,Screen.height/20,100,20),"Quit")) {
            Application.Quit();
    }
  
    GUI.Label(new Rect((float)(Screen.width/2 - 35),(float)(Screen.height/1.2 - 30),100,20),"Your Name:");
    playerName = GUI.TextField (new Rect ((float)(Screen.width/2 - 50),(float)(Screen.height/1.2),100,20), playerName, 12);
    }

    void startServer()
    {

        if (serverPass != "")
        {

            Network.incomingPassword = serverPass;
        }

        Network.InitializeServer((int)15, (int)25001, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, serverName, serverInfo);
    }

    void OnServerInitialized()
    {
        DontDestroyOnLoad(transform.gameObject);
        Application.LoadLevel("Lobby");
        lobbySpawn();
    }

    void OnConnectedToServer()
    {
        lobbySpawn();
    }

    void lobbySpawn(){

        //wait 0.1 s??
        GameObject newPlayer = (GameObject)Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);
 
        this.GetComponent(playerMove).playerName = playerName;
 
        PlayerPrefs.SetString("Player Name", playerName);
        if(Network.isClient){
            Destroy(this);
        }
}

    function refreshHostList()
    {

        MasterServer.ClearHostList();
        MasterServer.RequestHostList(gameName);
        refreshing = true;
    }

}
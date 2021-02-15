using Photon.Pun;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class networkController : MonoBehaviourPunCallbacks
{
    public networkControllerJoin joinControl;
    string gameVersion = "1";
    public PhotonView photonView;
    int xprev = 0;
    int yprev = 0;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void joinRoom(string roomName)
    {
        bool temp = PhotonNetwork.JoinRoom(roomName);
        Debug.Log("Ts");
    }
    public void createGame(string name)
    {
        Debug.Log(name);
        PhotonNetwork.CreateRoom(name,
        new RoomOptions()
        {
            MaxPlayers = 2,
            PublishUserId = true,
            IsVisible = true,
            PlayerTtl = 0,
            EmptyRoomTtl = 0
        }, null);
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene(sceneName: "Scenes/SampleScene");
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(sceneName: "Scenes/SampleScene");
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("entered Room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    public void sendEvent(Vector3Int pos, string obj, bool delete1 = false)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("test", RpcTarget.Others, pos.x, pos.y, obj, delete1);
    }

    [PunRPC]
    public void test(int xpos, int ypos, string obj, bool delete1)
    {

        tileMap tileControl = GameObject.Find("map").GetComponent<tileMap>();
        if (obj == "wall")
        {
            tileControl.dWall(xprev, yprev);
            tileControl.buildWall(xpos, ypos, false);
            xprev = xpos;
            yprev = ypos;
        }
        else if (obj == "road")
        {
            tileControl.dRoad(xprev, yprev);
            tileControl.buildRoad(xpos, ypos, false);
            xprev = xpos;
            yprev = ypos;
        }
        else if (obj == "house")
        {
            tileControl.dHouse(xprev, yprev);
            tileControl.buildHouse(xpos, ypos, false);
            xprev = xpos;
            yprev = ypos;
            
        }
    }

    // Update is called once per frame
    void Update()
   {
        
   }
}


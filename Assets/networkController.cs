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
    public void sendEvent()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("test", RpcTarget.Others);
    }

    [PunRPC]
    public void test()
    {
        Debug.Log("From user!?!?!");
    }

    // Update is called once per frame
    void Update()
   {

   }
}


using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;


public class networkController : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    public Text createGameField;
    //private RoomInfo[] roomsList;
    // Start is called before the first frame update
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
       //PhotonNetwork.AutomaticallySyncScene = true;s
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    void createGame()
    {
        UnityEngine.UI.InputField inputFieldGameName = GameObject.FindGameObjectWithTag("inputFieldGameName").GetComponent<InputField>();
        PhotonNetwork.CreateRoom(inputFieldGameName.text,
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
        UnityEngine.UI.Button createGameButton1 = GameObject.FindGameObjectWithTag("createGameButton1").GetComponent<Button>();
        createGameButton1.onClick.AddListener(() => createGame());
        //Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        
        //roomsList = PhotonNetwork.GetRoomList();

    }

    public override void OnCreatedRoom()
    {
        
    }

    public override void OnJoinedRoom()
    {
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

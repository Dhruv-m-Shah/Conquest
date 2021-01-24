using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


public class networkController : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    //private RoomInfo[] roomsList;
    // Start is called before the first frame update
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
       //PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        PhotonNetwork.CreateRoom("assadads",
        new RoomOptions()
        {
            MaxPlayers = 2,
            PublishUserId = true,
            IsVisible = true,
            PlayerTtl = 0,
            EmptyRoomTtl = 0
        }, null);
        //roomsList = PhotonNetwork.GetRoomList();

    }

    public override void OnCreatedRoom()
    {
        Debug.Log("TEST123");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("test");
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

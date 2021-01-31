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
    string gameVersion = "1";
    public Text createGameField;
    //private RoomInfo[] roomsList;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
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
    }

    public override void OnCreatedRoom()
    {
        
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(sceneName: "Scenes/SampleScene");
        Debug.Log("Joined");
        
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("entered Room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
    public void sendEvent(object[] data, byte eventId)
    {
        PhotonNetwork.RaiseEvent(eventId, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        Debug.Log("TEST567");
    }

    void OnEvent(EventData photonEvent)
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}

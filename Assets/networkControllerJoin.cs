using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class networkControllerJoin : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    void joinRoom()
    {
        PhotonNetwork.JoinRoom("abc");
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        UnityEngine.UI.Button joinGame = GameObject.FindGameObjectWithTag("joinGame").GetComponent<Button>();
        //asdasd
        joinGame.onClick.AddListener(() => joinRoom());
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");

        //roomsList = PhotonNetwork.GetRoomList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("JOINED A ROOM!!");
        //SceneManager.LoadScene(sceneName: "Scenes/SampleScene");

    }
}

using Photon.Pun;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class networkControllerInGame : MonoBehaviour
{

    public void sendEvent(object[] data, byte eventId)
    {
        PhotonNetwork.RaiseEvent(eventId, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
        Debug.Log("TEST567");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

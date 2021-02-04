using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class createGame : MonoBehaviour
{
    private networkController networkControl;
    //Start is called before the first frame update
    void createGame1()
    {
        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
        networkControl.createGame("abc");
    }
    void Start()
    {
        UnityEngine.UI.Button createGame123 = GameObject.Find("createGameButton").GetComponent<Button>();

        createGame123.onClick.AddListener(() => createGame1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

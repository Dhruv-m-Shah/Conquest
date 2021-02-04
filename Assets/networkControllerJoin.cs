using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class networkControllerJoin : MonoBehaviourPunCallbacks
{
    public networkController networkControl;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void joinScene()
    {
        SceneManager.LoadScene(sceneName: "Scenes/SampleScene");
    }

    void joinRoom()
    {
        networkControl.joinRoom("abc");
    }
    void Start()
    {

        UnityEngine.UI.Button joinGame = GameObject.FindGameObjectWithTag("joinGame").GetComponent<Button>();
        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
        joinGame.onClick.AddListener(() => joinRoom());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class networkControllerCreate : MonoBehaviour
{
    private networkController networkControl;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    //        UnityEngine.UI.InputField inputFieldGameName = GameObject.FindGameObjectWithTag("inputFieldGameName").GetComponent<InputField>();

    void createGame1()
    {
        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
    }
    void Start()
    {
        UnityEngine.UI.Button createGame = GameObject.FindGameObjectWithTag("createGameButton").GetComponent<Button>();
 
        createGame.onClick.AddListener(() => createGame1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

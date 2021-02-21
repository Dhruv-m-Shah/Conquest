using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class lobby : MonoBehaviour
{
    public networkController networkControl;
    public Text playerName;
    public Text opponentName;
    public Dropdown playerTurn;
    // Start is called before the first frame update

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }
    public void displayPlayerId(string id)
    {
        Debug.Log(id);
        playerName = GameObject.FindGameObjectWithTag("lobbyName1").GetComponent<Text>();
        playerName.text += " " + id;
    }

    public void displayOpponentId(string id)
    {
        Debug.Log(id);
        opponentName = GameObject.FindGameObjectWithTag("lobbyName2").GetComponent<Text>();
        opponentName.text += " " + id;
    }

    public void setDropdown(int value)
    {
        playerTurn.value = value;
    }

    void Start()
    {
        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
        networkControl.addPlayerToGame();
        playerTurn.onValueChanged.AddListener(delegate {
            networkControl.syncPlayerDropdown(playerTurn.value);
        });
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

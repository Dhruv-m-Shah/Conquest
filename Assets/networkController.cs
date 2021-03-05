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
    public lobby gameLobby;
    int xprev = 0;
    int yprev = 0;
    public playerController reference;
    // Start is called before the first frame update
    public playerController getGameInstance()
    {
        return reference;
    }
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
        reference = GameObject.Find("playerController").GetComponent<playerController>();
        PhotonNetwork.ConnectUsingSettings();
        //Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene(sceneName: "gameLobby");
    }
    public void addPlayerToGame()
    {
        gameLobby = GameObject.Find("CanvasLobby").GetComponent<lobby>();
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2) // Second player joined game.
        {
            string opponent = PhotonNetwork.PlayerListOthers[0].UserId;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if(player.UserId == opponent)
                {
                    reference.setOpponent(player.UserId);
                    reference.setHost(player.UserId);
                    gameLobby.displayPlayerId(player.UserId);
                }
                else
                {
                    reference.setPlayer(player.UserId);
                    gameLobby.displayOpponentId(player.UserId);
                }
                
            }
        }
        else {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                reference.setPlayer(player.UserId);
                reference.setHost(player.UserId);
                gameLobby.displayPlayerId(player.UserId);
            }
        }


    }

    public void setHostFirst()
    {
        reference.setHostFirst();
    }

    public void setHostSecond()
    {
        reference.setHostSecond();
    }
    public override void OnJoinedRoom()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log(player);
        }
        SceneManager.LoadScene(sceneName: "gameLobby");
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("Opponent Entered!!");
        reference.setOpponent(other.UserId);
        gameLobby.displayOpponentId(other.UserId);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    public void sendEvent(Vector3Int pos, string obj, bool delete1 = false, bool permenant = false, int direction = 0)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("syncPlayerBuild", RpcTarget.Others, pos.x, pos.y, obj, delete1, direction, permenant); 
    }

    public void changeTurn(Vector3Int prev, string curObject)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("changeTurnNetwork", RpcTarget.Others, prev.x, prev.y, curObject);
    }

    public void syncPlayerDropdown(int value)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("syncPlayerDropdownNetwork", RpcTarget.Others, value);
    }

    public void startGame()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("startGameNetwork", RpcTarget.Others);
    }

    [PunRPC]
    void changeTurnNetwork(int x, int y, string curObject)
    {
        tileMap tileMapRef = GameObject.Find("map").GetComponent<tileMap>();
        tileMapRef.destroyObject(x, y, curObject);
        reference.changeTurn();
    }

    [PunRPC]
    void startGameNetwork()
    {
        gameLobby.startGame();
    }

    [PunRPC]
    public void syncPlayerDropdownNetwork(int value)
    {
        gameLobby.setDropdown(value);
    }

    [PunRPC]
    public void syncPlayerBuild(int xpos, int ypos, string obj, bool delete1, int direction, bool perm)
    {
        if (!GameObject.Find("map")) // Player has not entered game yet;
        {
            return;
        }
        tileMap tileControl = GameObject.Find("map").GetComponent<tileMap>();
        if (obj == "wall")
        {
            if (delete1)
            {
                tileControl.dWall(xpos, ypos, direction);
            }
            else
            {
                tileControl.buildWall(xpos, ypos, perm, direction);
            }
        }
        else if (obj == "road")
        {
            if (delete1)
            {
                tileControl.dRoad(xpos, ypos, direction);
            }
            else
            {
                tileControl.buildRoad(xpos, ypos, perm, direction);
            }
        }
        else if (obj == "house")
        {
            if (delete1)
            {
                tileControl.dHouse(xpos, ypos, direction);
            }
            else
            {
                tileControl.buildHouse(xpos, ypos, perm, direction);
            }
        }
    }

    [PunRPC]
    public void syncPlayerBuildPerm(int xpos, int ypos, int zpos, string obj)
    {
        tileMap tileControl = GameObject.Find("map").GetComponent<tileMap>();
        tileControl.addTileOpponent(xpos, ypos, zpos);
    }

    string serializeVectorList(List<Vector3Int> blocks)
    {
        string serializeStr = "";
        foreach (Vector3Int part in blocks)
        {
            serializeStr += part.x.ToString() + ",";
            serializeStr += part.y.ToString() + ",";
            serializeStr += part.z.ToString() + ",";
        }
        return serializeStr;
    }

    public void addBlock(string blockType, List<Vector3Int> blocks)
    {
        PhotonView photonView = PhotonView.Get(this);
        string serialized = serializeVectorList(blocks);
        photonView.RPC("addBlockNetwork", RpcTarget.Others, blockType, serialized);
    }

    public List<Vector3Int> deserializeString(string serializedStr)
    {
        int count = 0;
        int x = 0;
        int y = 0;
        int z = 0;
        List<Vector3Int> newList = new List<Vector3Int>();
        for(int i = 0; i < serializedStr.Length; i++)
        {
            if(serializedStr[i] == ',')
            {
                count += 1;
                if (count % 3 == 0)
                {
                    count = 0;
                    newList.Add(new Vector3Int(x, y, z));
                }
            }
            else
            {
                if (count == 0)
                {
                    x = System.Convert.ToInt32(serializedStr[i]);
                }
                if (count == 1)
                {
                    y = System.Convert.ToInt32(serializedStr[i]);
                }
                if (count == 2)
                {
                    z = System.Convert.ToInt32(serializedStr[i]);
                }
            }
        }
        return newList;
    }

    [PunRPC]
    public void addBlockNetwork(string blockType, string serializedStr)
    {
        tileMap tileControl = GameObject.Find("map").GetComponent<tileMap>();
        List<Vector3Int> blocks = deserializeString(serializedStr);
        reference.addBlock(blockType, blocks, "opponent");
    } 

    // Update is called once per frame
    void Update()
   {
        
   }
}


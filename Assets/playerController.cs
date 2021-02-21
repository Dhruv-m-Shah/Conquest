using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game
{
    Players player = null;
    Players opponent = null;
    string turn = "";
    public void setPlayer(Players player)
    {
        this.player = player;
    }
    public void setOpponent(Players player)
    {
        Debug.Log(player.getId());
        this.opponent = player;
    }
    public void changeTurn()
    {
        player.setTurn(!player.getTurn());
        opponent.setTurn(!opponent.getTurn());
    }
}
public class Players
{
    string playerId;
    int baseHealth = 100;
    int food = 100;
    int gold = 100;
    int stone = 100;
    bool isHost = false;
    bool turn;
    // Base stats;
    HashSet<Vector3Int> taken;
    public bool getTurn()
    {
        return this.turn;
    }
    public void setTurn(bool val)
    {
        this.turn = val;
    }
    public Players(string id)
    {
        playerId = id;
        taken = new HashSet<Vector3Int>();
    }
    public bool inHashSet(Vector3Int point)
    {
        return this.taken.Contains(point);
    }
    public bool addInHashSet(Vector3Int point)
    {
        if (taken.Contains(point)) return false;
        taken.Add(point);
        return true;
    }
    public string getId()
    {
        return this.playerId;
    }
    
    public void setHost()
    {
        this.isHost = true;
    }

    public bool getHost()
    {
        return this.isHost;
    }
}


public class playerController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    Players player;
    Players opponent;
    Game game;
    // Start is called before the first frame update
    public bool addPoint(Vector3Int point)
    {
        return player.addInHashSet(point);
    }

    public bool addPointOpponent(Vector3Int point)
    {
        return opponent.addInHashSet(point);
    }

    public bool inHashSet(Vector3Int point)
    {
        return player.inHashSet(point) || opponent.inHashSet(point);
    }

    void Start()
    {
        game = new Game();
        player = new Players("player");
        opponent = new Players("opponent");
    }

    public void setPlayer(string id) // id is set to PUN2 userid.
    {
        player = new Players(id);
        game.setPlayer(player);
    }

    public void setOpponent(string id)
    {
        opponent = new Players(id);
        game.setOpponent(opponent);
    }

    public void setHost(string playerId)
    {
        if(player.getId() == playerId)
        {
            player.setHost();
        }
        else
        {
            opponent.setHost();
        }
    }


    public void setHostFirst()
    {
        if (player.getHost())
        {
            player.setTurn(true);
            opponent.setTurn(false);
        }
        else
        {
            player.setTurn(false);
            opponent.setTurn(true);
        }
    }

    public void setHostSecond()
    {
        if (player.getHost())
        {
            player.setTurn(false);
            opponent.setTurn(true);
            
        }
        else
        {
            player.setTurn(true);
            opponent.setTurn(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

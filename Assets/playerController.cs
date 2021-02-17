using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game
{
    Players player = null;
    Players opponent = null;
    public void setPlayer(Players player)
    {
        this.player = player;
    }
    public void setOpponent(Players player)
    {

    }
}
public class Players
{
    string playerId;
    int baseHealth = 100;
    int food = 100;
    int gold = 100;
    int stone = 100;
    // Base stats;
    HashSet<Vector3Int> taken;
    
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
}


public class playerController : MonoBehaviour
{
    Players player;
    Game game;
    // Start is called before the first frame update
    public bool addPoint(Vector3Int point)
    {
        return player.addInHashSet(point);
    }
    public bool inHashSet(Vector3Int point)
    {
        return player.inHashSet(point);
    }
    void Start()
    {
        
    }

    void setPlayer(string id)
    {
        player = new Players("player1");
        game.setPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

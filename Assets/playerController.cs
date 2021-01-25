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
    }
    public bool inHashSet(Vector3Int point)
    {
        return this.taken.Contains(point);
    }
    public bool addInHashSet(Vector3Int point)
    {
        if (this.taken.Contains(point)) return false;
        this.taken.Add(point);
        return true;
    }
}


public class playerController : MonoBehaviour
{
    Players player;
    // Start is called before the first frame update
    bool addPoint(Vector3Int point)
    {
        return player.addInHashSet(point);
    }
    void Start()
    {
        Game game = new Game();
        Players player = new Players("player1");
        game.setPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

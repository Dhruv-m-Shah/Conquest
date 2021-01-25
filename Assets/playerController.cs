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
    public Players(string id)
    {
        playerId = id;
    }
}

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
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

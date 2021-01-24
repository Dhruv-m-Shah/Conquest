using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    string playerId;
    int baseHealth = 100;
    int food = 100;
    int gold = 100;
    int stone = 100;
    // Base stats;
    public Player(string id)
    {
        playerId = id;
    }
}

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

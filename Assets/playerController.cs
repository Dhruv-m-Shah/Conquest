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
    Dictionary<string, int> materials;
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
        materials = new Dictionary<string, int>();
        materials.Add("food", 100);
        materials.Add("gold", 100);
        materials.Add("stone", 100);
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

    public int getMaterialValue(string material) {
        if (!materials.ContainsKey(material))
        {
            return -1;
        }
        else
        {
            return materials[material];
        }
    }

    public void decreaseMaterials(string resource, int value)
    {
        materials[resource] -= value;
    }
}


public class playerController : MonoBehaviour
{
    Dictionary<string, int> road = new Dictionary<string, int>();
    Dictionary<string, int> wall = new Dictionary<string, int>();
    Dictionary<string, Dictionary<string, int>> materialsDictionary = new Dictionary<string, Dictionary<string, int>>();
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

    public void initializeMaterialsDict()
    {
        road.Add("food", 10);
        road.Add("stone", 10);
        wall.Add("food", 20);
        wall.Add("stone", 20);
        materialsDictionary.Add("road", road);
        materialsDictionary.Add("wall", wall);
    }
    void Start()
    {
        game = new Game();
        player = new Players("player");
        opponent = new Players("opponent");
        initializeMaterialsDict();
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

    public bool myTurn()
    {
        return player.getTurn();
    }

    public void changeTurn()
    {
        game.changeTurn();
    }

    public void changeHealthPlayer(int decrease) 
    {
        playerStats stats = GameObject.FindGameObjectWithTag("healthBar").GetComponent<playerStats>();
        stats.changeHealthUI(decrease);
    }

    public void changeHealthOpponent(int decrease)
    {

    }

    public bool enoughMaterials(string material)
    {
        Debug.Log(material);
        foreach (KeyValuePair<string, int> kvp in materialsDictionary[material])
        {
            Debug.Log("2");
            if (player.getMaterialValue(kvp.Key) < kvp.Value) return false;
        }
        return true;
    }

    public void decreaseMaterials(string material)
    {
        tileMap tiles = GameObject.Find("map").GetComponent<tileMap>();
        
        foreach (KeyValuePair<string, int> kvp in materialsDictionary[material])
        {
           
            player.decreaseMaterials(kvp.Key, kvp.Value);
            tiles.updateMaterialUI(kvp.Key, kvp.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

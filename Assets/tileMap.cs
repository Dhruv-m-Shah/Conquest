using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class tileMap : MonoBehaviour
{
    public Tilemap test;
    public Tile test1;
    public Tile brick;
    public Tile road;
    public Tile wall;
    public Vector3Int prev;
    public playerController other;
    public networkController networkControl;
    CanvasGroup buildWindow;
    private bool _isBuildOpen = false;
    private const byte syncBuild = 0;
    int direction = 0;
    string curObject = "house";
    public CanvasGroup panel;

    public void addTileOpponent(int xpos, int ypos, int zpos)
    {
        Vector3Int temp = new Vector3Int(xpos, ypos, zpos);
        other.addPointOpponent(temp);
    }

    public bool buildHouse(int topLeftX, int topLeftY, bool onClick=false, int direction = 0, string whichPlayer = "player")
    {
        List<Vector3Int> temp1 = new List<Vector3Int>();
        bool flag = false;
        for (int k = 1; k < 3; k++)
        {
            for(int i = topLeftX; i < topLeftX + 3; i++)
            {
                for(int j = topLeftY; j < topLeftY + 3; j++)
                {
                    Vector3Int p = new Vector3Int(i-k, j-k, k*5);
                    Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90), direction);
                    Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                    temp1.Add(rotated);
                }
            }
        }
        foreach (Vector3Int part in temp1)
        {
            if (other.inHashSet(part)) flag = true;
        }
        if (!flag)
        {
            foreach (Vector3Int part in temp1)
            {
                if (onClick)
                {
                    other.addPoint(part);
                }
                test.SetTile(part, brick);
            }
            other.addBlock("house", temp1, whichPlayer);
            if(whichPlayer == "player") networkControl.addBlock("house", temp1);

        }
        else
        {
            foreach (Vector3Int part in temp1)
            {
                if (other.inHashSet(part)) continue;
                test.SetTile(part, brick);
                test.SetTileFlags(part, TileFlags.None);
                test.SetColor(part, Color.red);
            }
        }
        return !flag;
    }

    public void dHouse(int topLeftX, int topLeftY, int direction = 0)
    {
        for (int k = 1; k < 3; k++)
        {
            for (int i = topLeftX; i < topLeftX + 3; i++)
            {
                for (int j = topLeftY; j < topLeftY + 3; j++)
                {
                    Vector3Int p = new Vector3Int(i - k, j - k, k * 5);
                    Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90), direction);
                    Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                    if (other.inHashSet(rotated)) continue;
                    test.SetTile(rotated, null);
                }
            }
        }
    }

    public bool buildRoad(int topLeftX, int topLeftY, bool onClick=false, int direction = 0, string whichPlayer = "player")
    {
        List<Vector3Int> temp1 = new List<Vector3Int>();
        bool flag = false;
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i-1, j-1, 4);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, 4), new Vector3Int(topLeftX, topLeftY, 4), new Vector3Int(0, 0, 90), direction);
                Vector3Int rotated = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
                temp1.Add(rotated);
            }
        }
        foreach (Vector3Int part in temp1)
        {
            if (other.inHashSet(part)) flag = true;
        }
        if (!flag)
        {
            foreach (Vector3Int part in temp1)
            {
                if (onClick)
                {
                    other.addPoint(part);
                }
                test.SetTile(part, road);
            }
            other.addBlock("house", temp1, whichPlayer);
        }
        else
        {
            foreach (Vector3Int part in temp1)
            {
                if (other.inHashSet(part)) continue;
                test.SetTile(part, road);
                test.SetTileFlags(part, TileFlags.None);
                test.SetColor(part, Color.red);
            }
        }
        return !flag;
    }

    public void dRoad(int topLeftX, int topLeftY, int direction  = 0)
    {
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i - 1, j - 1, 4);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, 4), new Vector3Int(topLeftX, topLeftY, 4), new Vector3Int(0, 0, 90), direction);
                Vector3Int rotated = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
                if (other.inHashSet(rotated)) continue;
                test.SetTile(rotated, null);
            }
        }
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles, int direction = 0)
    {
        for (int i = 0; i < direction + 1; i++)
        {
            Vector3 dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
        }
        return point; // return it
    }

    public bool buildWall(int topLeftX, int topLeftY, bool onClick=false, int direction = 0, string whichPlayer = "player")
    {
        List<Vector3Int> temp1 = new List<Vector3Int>();
        bool flag = false;
        for (int k = 1; k < 3; k++)
        {
            for(int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(topLeftX-k, j-k, k * 5);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(topLeftX, j, k*5), new Vector3Int(topLeftX, topLeftY, k*5), new Vector3Int(0, 0, 90), direction);
                Vector3Int rotated = new Vector3Int((int)temp.x-k, (int)temp.y-k, (int)temp.z);
                temp1.Add(rotated);
            }
        }
        foreach (Vector3Int part in temp1)
        {
            if (other.inHashSet(part)) flag = true;
        }
        if (!flag)
        {
            foreach(Vector3Int part in temp1)
            {
                if (onClick)
                {
                    other.addPoint(part);
                }
                test.SetTile(part, wall);
            }
            other.addBlock("house", temp1, whichPlayer);
        }
        else
        {
            foreach (Vector3Int part in temp1)
            {
                if (other.inHashSet(part)) continue;
                test.SetTile(part, wall);
                test.SetTileFlags(part, TileFlags.None);
                test.SetColor(part, Color.red);
            }
        }
        return !flag;
    }

    public void dWall(int topLeftX, int topLeftY, int direction = 0)
    {
        for (int k = 1; k < 3; k++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(topLeftX - k, j - k, k * 5);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(topLeftX, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90), direction);
                Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                if (other.inHashSet(rotated)) continue;
                test.SetTile(rotated, null);
            }
        }
    }

    public void updateMaterialUI(string material, int value)
    {
        Debug.Log(material);
        Text textVal = GameObject.Find(material).GetComponent<Text>();
        textVal.text = (System.Convert.ToInt32(textVal.text) - value).ToString();
    }

    void showBuildWindow()
    {
        buildWindow.alpha = 1;
        buildWindow.interactable = true;
    }

    void closeBuildWindow()
    {
        buildWindow.alpha = 0;
        buildWindow.interactable = false;
    }

    void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, 1); // back to solid
        float width = GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth;
        float height = GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight;
    }
    void TaskOnClick(string buildingType, Vector3Int prev)
    {
        destroyObject(prev.x, prev.y);
        curObject = buildingType;
    }
    void endTurn(Vector3Int prev)
    {
        if (other.myTurn())
        {
            destroyObject(prev.x, prev.y);
            other.changeTurn();
            networkControl.changeTurn(prev, curObject);
        }
    }
    void Start()
    {
        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
        other = networkControl.getGameInstance();
        buildWindow = GameObject.FindGameObjectWithTag("buildPanel").GetComponent<CanvasGroup>();
        buildWindow.alpha = 0;
        buildWindow.interactable = false;
        UnityEngine.UI.Button houseButton = GameObject.FindGameObjectWithTag("button").GetComponent<Button>();
        UnityEngine.UI.Button roadButton = GameObject.FindGameObjectWithTag("roadButton").GetComponent<Button>();
        UnityEngine.UI.Button wallButton = GameObject.FindGameObjectWithTag("wallButton").GetComponent<Button>();
        UnityEngine.UI.Button buildButton = GameObject.FindGameObjectWithTag("buildButton").GetComponent<Button>();
        UnityEngine.UI.Button closeBuildButton = GameObject.FindGameObjectWithTag("closeBuildButton").GetComponent<Button>();
        UnityEngine.UI.Button endTurnButton = GameObject.FindGameObjectWithTag("endTurn").GetComponent<Button>();
        closeBuildButton.onClick.AddListener(() => closeBuildWindow());
        buildButton.onClick.AddListener(() => showBuildWindow());
        houseButton.onClick.AddListener(() => TaskOnClick("house", prev));
        roadButton.onClick.AddListener(() => TaskOnClick("road", prev));
        wallButton.onClick.AddListener(() => TaskOnClick("wall", prev));
        endTurnButton.onClick.AddListener(() => endTurn(prev));
        prev = new Vector3Int(0, 0, 0);
        GameObject.Find("Main Camera").transform.position = new Vector3(10, 11.5f, -10);
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 14;
        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            { 
                Vector3Int p = new Vector3Int(x, y, 0);
                bool odd = (x + y) % 2 == 1;
                Tile tile = test1;
                test.SetTile(p, tile);
            }
        }
        Vector3Int temp = new Vector3Int(0, 0, 0);
        test.SetTileFlags(temp, TileFlags.None);
        test.SetColor(temp, Color.red);
    }

    bool buildObject(int xpos, int ypos, bool onClick=false)
    {
        if(curObject == "wall")
        {
            return buildWall(xpos, ypos, onClick, direction);
        }
        else if(curObject == "road")
        {
            return buildRoad(xpos, ypos, onClick, direction);
        }
        else if(curObject == "house")
        {
            return buildHouse(xpos, ypos, onClick, direction);
        }
        else
        {
            return false;
        }
    }

    public void destroyObject(int xpos, int ypos, string sync = "")
    {
        if (sync == "")
        {
            if (curObject == "wall")
            {
                dWall(xpos, ypos, direction);
            }
            else if (curObject == "road")
            {
                dRoad(xpos, ypos, direction);
            }
            else if (curObject == "house")
            {
                dHouse(xpos, ypos, direction);
            }
        }
        else
        {
            if (sync == "wall")
            {
                dWall(xpos, ypos, direction);
            }
            else if (sync == "road")
            {
                dRoad(xpos, ypos, direction);
            }
            else if (sync == "house")
            {
                dHouse(xpos, ypos, direction);
            }
        }
    }

    bool checkMaterials(string curObject)
    {
        if (other.enoughMaterials(curObject))
        {
            other.decreaseMaterials(curObject);
            return true;
        }
        return false;
    }
    // Update is called once per frame.
    void Update()
    {
        if (!other.myTurn())
        {
            return;
        }
        if (Input.GetKeyDown("space"))
        {
            destroyObject(prev.x, prev.y);
            networkControl.sendEvent(prev, curObject, true, false, direction);
            direction = (direction + 1) % 4;
        }

        Vector3 test123 = RotatePointAroundPivot(new Vector3Int(1, 2, 0), new Vector3Int(0, 0, 0), new Vector3Int(0, 0, 90), direction);
        Vector3 point = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3Int selectedTile = test.WorldToCell(point);
        selectedTile.z += 10;
        selectedTile.x -= 6;
        selectedTile.y -= 6;
        test.SetTileFlags(selectedTile, TileFlags.None);
        if (Input.GetMouseButtonDown(1))
        {
            if (curObject == "house" || checkMaterials(curObject)) //If curobject is house, no resources are used, and no need to checl.
            {
                if (buildObject(selectedTile.x, selectedTile.y, true))
                {
                    networkControl.sendEvent(selectedTile, curObject, false, true);
                }
            }
        }
        if (selectedTile.x < 50 && selectedTile.x >= 0 && selectedTile.y < 50 && selectedTile.y >= 0)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (x == selectedTile.x && y == selectedTile.y)
                    {
                        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
                        destroyObject(prev.x, prev.y);
                        networkControl.sendEvent(prev, curObject, true, false, direction);
                        buildObject(x, y);
                        networkControl.sendEvent(selectedTile, curObject, false, false, direction);
                    }
                }
            }
            prev = selectedTile;
        }
    }
}

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

    public bool buildHouse(int topLeftX, int topLeftY, bool onClick=false)
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
                    Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90));
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
                if (onClick) other.addPoint(part);
                test.SetTile(part, brick);
            }

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

    public void dHouse(int topLeftX, int topLeftY )
    {
        for (int k = 1; k < 3; k++)
        {
            for (int i = topLeftX; i < topLeftX + 3; i++)
            {
                for (int j = topLeftY; j < topLeftY + 3; j++)
                {
                    Vector3Int p = new Vector3Int(i - k, j - k, k * 5);
                    Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90));
                    Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                    if (other.inHashSet(rotated)) continue;
                    test.SetTile(rotated, null);
                }
            }
        }
    }

    public bool buildRoad(int topLeftX, int topLeftY, bool onClick=false)
    {
        List<Vector3Int> temp1 = new List<Vector3Int>();
        bool flag = false;
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i-1, j-1, 4);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, 4), new Vector3Int(topLeftX, topLeftY, 4), new Vector3Int(0, 0, 90));
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
                if (onClick) other.addPoint(part);
                test.SetTile(part, road);
            }

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

    public void destroyRoad(int topLeftX, int topLeftY)
    {
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i - 1, j - 1, 4);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, 4), new Vector3Int(topLeftX, topLeftY, 4), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
                if (other.inHashSet(rotated)) continue;
                test.SetTile(rotated, null);
            }
        }
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        for (int i = 0; i < direction + 1; i++)
        {
            Vector3 dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
        }
        return point; // return it
    }

    public bool buildWall(int topLeftX, int topLeftY, bool onClick=false, bool redColor = false)
    {
        List<Vector3Int> temp1 = new List<Vector3Int>();
        bool flag = false;
        for (int k = 1; k < 3; k++)
        {
            for(int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(topLeftX-k, j-k, k * 5);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(topLeftX, j, k*5), new Vector3Int(topLeftX, topLeftY, k*5), new Vector3Int(0, 0, 90));
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
                if (onClick) other.addPoint(part);
                test.SetTile(part, wall);
            }
            
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
    //test
    public void destroyWall(int topLeftX, int topLeftY)
    {
        for (int k = 1; k < 3; k++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(topLeftX - k, j - k, k * 5);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(topLeftX, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                if (other.inHashSet(rotated)) continue;
                test.SetTile(rotated, null);
            }
        }
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
    void Start()
    {
        buildWindow = GameObject.FindGameObjectWithTag("buildPanel").GetComponent<CanvasGroup>();
        buildWindow.alpha = 0;
        buildWindow.interactable = false;
        UnityEngine.UI.Button houseButton = GameObject.FindGameObjectWithTag("button").GetComponent<Button>();
        UnityEngine.UI.Button roadButton = GameObject.FindGameObjectWithTag("roadButton").GetComponent<Button>();
        UnityEngine.UI.Button wallButton = GameObject.FindGameObjectWithTag("wallButton").GetComponent<Button>();
        UnityEngine.UI.Button buildButton = GameObject.FindGameObjectWithTag("buildButton").GetComponent<Button>();
        UnityEngine.UI.Button closeBuildButton = GameObject.FindGameObjectWithTag("closeBuildButton").GetComponent<Button>();
        closeBuildButton.onClick.AddListener(() => closeBuildWindow());
        buildButton.onClick.AddListener(() => showBuildWindow());
        houseButton.onClick.AddListener(() => TaskOnClick("house", prev));
        roadButton.onClick.AddListener(() => TaskOnClick("road", prev));
        wallButton.onClick.AddListener(() => TaskOnClick("wall", prev));
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
            return buildWall(xpos, ypos, onClick);
        }
        else if(curObject == "road")
        {
            return buildRoad(xpos, ypos, onClick);
        }
        else if(curObject == "house")
        {
            return buildHouse(xpos, ypos, onClick);
        }
        else
        {
            return false;
        }
    }

    void destroyObject(int xpos, int ypos)
    {
        if (curObject == "wall")
        {
            destroyWall(xpos, ypos);
        }
        else if (curObject == "road")
        {
            destroyRoad(xpos, ypos);
        }
        else if (curObject == "house")
        {
            dHouse(xpos, ypos);
        }
    }
    // Update is called once per frame.
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            destroyObject(prev.x, prev.y);
            direction = (direction + 1) % 4;
        }

        Vector3 test123 = RotatePointAroundPivot(new Vector3Int(1, 2, 0), new Vector3Int(0, 0, 0), new Vector3Int(0, 0, 90));
        Debug.Log(test123);
        Vector3 point = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3Int selectedTile = test.WorldToCell(point);
        selectedTile.z += 10;
        selectedTile.x -= 6;
        selectedTile.y -= 6;
        test.SetTileFlags(selectedTile, TileFlags.None);
        if (Input.GetMouseButtonDown(1))
        {
            buildObject(selectedTile.x, selectedTile.y, true);

        }
        if (selectedTile.x < 50 && selectedTile.x >= 0 && selectedTile.y < 50 && selectedTile.y >= 0)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (x == selectedTile.x && y == selectedTile.y)
                    {
                        
                        destroyObject(prev.x, prev.y);
                        object[] eventData = new object[] {x, y, buildObject(x, y), curObject};
                        networkControl = GameObject.Find("networkControl").GetComponent<networkController>();
                        networkControl.sendEvent(prev, curObject, true);
                        networkControl.sendEvent(selectedTile, curObject, false);
                    }
                }
            }
            prev = selectedTile;
        }
    }
}

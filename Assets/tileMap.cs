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
    private bool _isBuildOpen = false;
    int direction = 0;
    string curObject = "house";
    // Start is called before the first frame update
    void buildHouse(int topLeftX, int topLeftY)
    {
        for(int k = 1; k < 3; k++)
        {
            for(int i = topLeftX; i < topLeftX + 3; i++)
            {
                for(int j = topLeftY; j < topLeftY + 3; j++)
                {
                    Vector3Int p = new Vector3Int(i-k, j-k, k*5);
                    Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90));
                    Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                    test.SetTile(rotated, brick);
                }
            }
        }
    }

    void destroyHouse(int topLeftX, int topLeftY)
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
                    test.SetTile(rotated, null);
                }
            }
        }
    }

    void buildRoad(int topLeftX, int topLeftY)
    {
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i-1, j-1, 4);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, 4), new Vector3Int(topLeftX, topLeftY, 4), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
                test.SetTile(rotated, road);
            }
        }
    }

    void destroyRoad(int topLeftX, int topLeftY)
    {
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i - 1, j - 1, 4);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(i, j, 4), new Vector3Int(topLeftX, topLeftY, 4), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
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

    void buildWall(int topLeftX, int topLeftY)
    {
        for(int k = 1; k < 3; k++)
        {
            for(int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(topLeftX-k, j-k, k * 5);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(topLeftX, j, k*5), new Vector3Int(topLeftX, topLeftY, k*5), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp.x-k, (int)temp.y-k, (int)temp.z);
                test.SetTile(rotated, wall);
            }
        }
    }

    void destroyWall(int topLeftX, int topLeftY)
    {
        for (int k = 1; k < 3; k++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(topLeftX - k, j - k, k * 5);
                Vector3 temp = RotatePointAroundPivot(new Vector3Int(topLeftX, j, k * 5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp.x - k, (int)temp.y - k, (int)temp.z);
                test.SetTile(rotated, null);
            }
        }
    }

    void showBuildWindow()
    {
        Debug.Log("test");
        GUI.color = new Color(1, 1, 1, 1); // back to solid
        float width = GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth;
        float height = GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight;
        GUI.Box(new Rect(300, 20, width-600, height-250), "");
        GUI.Box(new Rect(300, 20, width - 600, height - 250), "");
        GUI.Box(new Rect(300, 20, width - 600, height - 250), "");
        GUI.Box(new Rect(300, 20, width - 600, height - 250), "");
        GUI.Box(new Rect(300, 20, width - 600, height - 250), "");
    }

    void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, 1); // back to solid
        float width = GameObject.Find("Main Camera").GetComponent<Camera>().pixelWidth;
        float height = GameObject.Find("Main Camera").GetComponent<Camera>().pixelHeight;
        GUI.Box(new Rect(0, height - 200, width, 200), "");
        if (GUI.Button(new Rect(50, height - 125, 100, 50), "Build"))
        {
            _isBuildOpen = !_isBuildOpen;
        };
        if (_isBuildOpen)
        {
            showBuildWindow();
        }
        GUI.Button(new Rect(200, height - 125, 100, 50), "End Turn");
        GUI.Button(new Rect(350, height - 125, 100, 50), "Quit Game");
    }
    void Start()
    {
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

    void buildObject(int xpos, int ypos)
    {
        if(curObject == "wall")
        {
            buildWall(xpos, ypos);
        }
        else if(curObject == "road")
        {
            buildRoad(xpos, ypos);
        }
        else if(curObject == "house")
        {
            buildHouse(xpos, ypos);
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
            destroyHouse(xpos, ypos);
        }
    }
    // Update is called once per frame
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
        if (selectedTile.x < 50 && selectedTile.x >= 0 && selectedTile.y < 50 && selectedTile.y >= 0)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    if (x == selectedTile.x && y == selectedTile.y)
                    {
                        
                        destroyObject(prev.x, prev.y);
                        //buildHouse(x, y);
                        buildObject(x, y);
                    }
                    else
                    {
                        //test.SetColor(new Vector3Int(x, y, 0), Color.white);
                    }
                }
            }
            prev = selectedTile;
        }
    }
}

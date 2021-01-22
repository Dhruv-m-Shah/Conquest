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
                    test.SetTile(p, brick);
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
                    test.SetTile(p, null);
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
                
                test.SetTile(p, road);
                
            }
        }
    }

    void destroyRoad(int topLeftX, int topLeftY)
    {
        for (int i = topLeftX; i < topLeftX + 2; i++)
        {
            for (int j = topLeftY; j < topLeftY + 2; j++)
            {
                Vector3Int p = new Vector3Int(i-1, j-1, 4);
                test.SetTile(p, null);
            }
        }
    }
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
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
                Vector3 temp1 = RotatePointAroundPivot(new Vector3Int((int)temp.x, (int)temp.y, k*5), new Vector3Int(topLeftX, topLeftY, k * 5), new Vector3Int(0, 0, 90));
                Vector3Int rotated = new Vector3Int((int)temp1.x-k, (int)temp1.y-k, (int)temp1.z);
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
                Vector3Int p = new Vector3Int(topLeftX-k, j-k, k * 5);
                test.SetTile(p, null);
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
    // Update is called once per frame
    void Update()
    {
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
                        destroyWall(prev.x, prev.y);
                        //buildHouse(x, y);
                        buildWall(x, y);
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

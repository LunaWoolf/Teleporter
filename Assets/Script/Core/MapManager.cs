using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class MapManager : MonoSingleton<MapManager>
{

    [Header("Prefab Reference")]
    public GameObject TilePrefab;
    public GameObject rootOfMap;

    [Header("General Reference")]
    public GameObject Player;

    public Tile[,] tileArray;

    void Awake()
    {
        if (!Player) Player = GameObject.FindGameObjectWithTag("Player");
    }
   
    void Start()
    {

        GenerateMap(20, 20);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap(int x, int y)
    {
        tileArray = new Tile[x,y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 pos = new Vector3(10 * i, 0, 10 * j);
                GameObject tileObject_temp = Instantiate(TilePrefab, rootOfMap.transform);
                Tile tile_temp = tileObject_temp.GetComponent<Tile>();
                tile_temp.SetTileType(Tile.Type.Building);
                tileObject_temp.transform.position = pos;
                tileArray[i, j] = tile_temp;
            }

        }
        BuildingMap(x, y);
    }

    void BuildingMap(int x, int y)
    {

        int i = 0, j = 0;
        Random rand = new Random();

        while (i < x && j < y)
        {
            //Random.seed = System.DateTime.Now.Millisecond;
            Tile tile_temp = tileArray[i, j];
            if (i == 0 && j == 0) // Start point
            {
                tile_temp.SetTileType(Tile.Type.Walkable);
                if (rand.Next(0, 2) < 1)
                    i++; // keep straight
                else
                    j++; // turn;
            }
            else
            {
                tile_temp.SetTileType(Tile.Type.Walkable);
                if (rand.Next(0, 2) < 1)
                    i++; // keep straight
                else
                    j++; // turn;
            }
        }


        /*
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Tile tile_temp = tileArray[i, j];

                if (i == 0 && j == 0) // Start point
                {
                    tile_temp.SetTileType(Tile.Type.Walkable);
                    break;
                }
                else
                {
                    CheckAdjacent(i, j);
                }

            }

        }*/

    }

    int CheckAdjacent(int x, int y)
    {

        return 0;
    }


}

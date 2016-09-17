using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
    [Serializable]

    //Class that acts as a range for a number of uses
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 100;                       //Columns on the board
    public int rows = 100;                          //Rows on the board
    public Count wallCount = new Count(20, 40 );    //Number of wall tiles
    public Count goldCount = new Count(10, 30);     //Number of gold tiles

    //Prefab slots, to fill in the listed prefab
    //Filled in the editor
    public GameObject door1;                        
    public GameObject player;

    //Prefab arrays, so the system can randomly select tiles by type for flavor.
    //Filled in the editor
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] goldTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    
    
    /// <summary>
    /// 2D array of the board, to be pulled for pathfinding, etc.
    /// Current tiles - 0 = floor, 1 = wall
    /// </summary>
    private int[,] boardMap;

    private Transform boardHolder;                              //Holds the parent transform of the board   
    private List<Vector2> gridPositions = new List<Vector2>();  //A list of grid coordinates, [0,0] to [columns,rows]

    /// <summary>
    /// Creates a list of grid coordinates, [0,0] to [columns,rows]
    /// </summary>
    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns -1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }

    /// <summary>
    /// Initializes board with floor tiles and outer wall tiles
    /// </summary>
    void BoardSetup()
    {
        boardMap = new int[columns, rows];

        boardHolder = new GameObject("Board").transform;

        //Loops through entire board, creating floor tiles and outer wall tiles
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    //boardMap[x, y] = 1;
                }
				else
					boardMap[x, y] = 0;
                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    /// <summary>
    /// Creates a random position in the grid
    /// </summary>
    /// <returns>A Vector2 representing a location on the board</returns>
    Vector2 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector2 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }


    /// <summary>
    /// Takes an array of tiles, and places a random number of them between minimum and maximum on the board in random locations.
    /// </summary>
    /// <param name="tileArray">Array of tiles to select from</param>
    /// <param name="minimum">Minimum number of tiles to place</param>
    /// <param name="maximum">Maximum number of tiles to place</param>
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector2 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }


   public void SetupScene (int level)
    {
        BoardSetup();           //Initialize board with floor/outer wall tiles
        InitializeList();       //Create the list of board positions
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);      //Place wall tiles
        LayoutObjectAtRandom(goldTiles, goldCount.minimum, goldCount.maximum);      //Place gold tiles
        int enemyCount = 1;//(int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);                   //Place enemies
        Instantiate(door1, new Vector2(columns - 1, rows - 1), Quaternion.identity);//Create the floor exit

    }
}

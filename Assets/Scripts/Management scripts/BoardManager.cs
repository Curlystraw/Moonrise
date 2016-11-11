﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Completed;

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

	//Temporary reference to the map generator. Shit's awkward.
	private MazeGenerator2 mapGen;


    private int columns = 100;                       //Columns on the board
    private int rows = 100;                          //Rows on the board
    public Count wallCount = new Count(20, 40 );    //Number of wall tiles
    public Count goldCount = new Count(10, 30);     //Number of gold tiles

    //Prefab slots, to fill in the listed prefab
    //Filled in the editor
    public GameObject door1;                        
    public GameObject player;
	public GameObject fog;

    //Prefab arrays, so the system can randomly select tiles by type for flavor.
    //Filled in the editor
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] goldTiles;
    public GameObject[] enemyTiles;
	public GameObject[] chestTiles;
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
        //boardMap = new int[columns, rows];

        boardHolder = new GameObject("Board").transform;
		mapGen = GetComponent<MazeGenerator2>();
		mapGen.Init();
		columns = mapGen.radius*3;
		rows = mapGen.radius*3;
		mapGen.GeneratePath();
		boardMap = mapGen.boardMap;

        //Loops through entire board, creating fog
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                
				GameObject f = Instantiate(fog, new Vector2(x,y), Quaternion.identity) as GameObject;
				f.transform.SetParent(this.transform);
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
	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum, bool blocking = false)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector2 randomPosition = RandomPosition();
			while(boardMap[(int)randomPosition.x,(int)randomPosition.y] > 0)
				randomPosition = RandomPosition();
			if(blocking)
				boardMap[(int)randomPosition.x,(int)randomPosition.y] = 1;
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			GameObject ob = (GameObject)Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }


   public void SetupScene (int level)
    {
        BoardSetup();           //Initialize board with floor/outer wall tiles
        InitializeList();       //Create the list of board positions
        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);      //Place wall tiles
        LayoutObjectAtRandom(goldTiles, goldCount.minimum, goldCount.maximum);      //Place gold tiles
		int chestCount = 7;
		LayoutObjectAtRandom (chestTiles, chestCount, chestCount);
        int enemyCount = 18;//(int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);                   //Place enemies
        Instantiate(door1, new Vector2(columns - 1, rows - 1), Quaternion.identity);//Create the floor exit

    }



	/// <summary>
	/// Finds a path between startLoc and target locations, using A*. Path is returned in the path list
	/// </summary>
	/// <param name="startLoc">Start location.</param>
	/// <param name="target">Target Location</param>
	public List<Vector2> findPath(Vector2 startLoc, Vector2 target){
		
		List<ASquare> open = new List<ASquare>();
		List<Vector2> closed = new List<Vector2>();
		List<Vector2> path = new List<Vector2>();

		ASquare current = new ASquare((startLoc-target).magnitude,0,startLoc);
		int maxSteps = 0;
		while(!current.loc.Equals(target) && maxSteps < 100){
			maxSteps++;
			Vector2[] moves = getAdjacentMoves(current.loc);
			for(int i = 0; i < 4; i++){
				Vector2 move = moves[i];
				if(!isClosed(move,closed)){
					ASquare newMove = new ASquare((move-target).magnitude,current.pathLength+1,move);
					newMove.setPrevious(current);
					open.Add(newMove);
				}
			}

			open.Sort();
			if (open.Count == 0)	// return empty list if there is no possible path
				return new List<Vector2> ();
			current = open[0];
			open.RemoveAt(0);
			closed.Add(current.loc);
		}


		while(current.getPrevious() != null){
			path.Add(current.loc);
			current = current.getPrevious();
		}


		return path;
	}

	/// <summary>
	/// Gets valid adjacent squares
	/// </summary>
	/// <returns>adjacent squares, in order [N,E,S,W]. Entry is null if not a valid move</returns>
	/// <param name="loc">Starting location</param>
	private Vector2[] getAdjacentMoves(Vector2 loc){
		//diretions
		Vector2[] moves = {new Vector2(-1,-1),new Vector2(-1,-1),new Vector2(-1,-1),new Vector2(-1,-1)};
		int x = (int)loc.x;
		int y = (int)loc.y;
		//Debug.Log(x+","+y);
		//North
		if(y != 0){
			if(boardMap[x,y-1] == 0)
				moves[0] = new Vector2(x,y-1);
		}
		//East
		if(x != columns-1){
			if(boardMap[x+1,y] == 0)
				moves[1] = new Vector2(x+1,y);
		}
		//South
		if(y != rows-1){
			if(boardMap[x,y+1] == 0)
				moves[2] = new Vector2(x,y+1);
		}
		//West
		if(x != 0){
			if(boardMap[x-1,y] == 0)
				moves[3] = new Vector2(x-1,y);
		}

		return moves;
	}

	private bool isClosed(Vector2 loc, List<Vector2> closed){
		if(loc.x == -1)
			return true;
		for(int i = 0; i < closed.Count; i++){
			if(loc.Equals(closed[i]))
				return true;
		}
		return false;
	}
}


public class ASquare : IComparable<ASquare>{
	public float cost;
	public int pathLength;
	public Vector2 loc;
	private ASquare previous;

	public ASquare(float d, int len, Vector2 l){
		cost = len+d;
		pathLength = len;
		loc = l;
	}

	public void setPrevious(ASquare a){
		previous = a;
	}

	public ASquare getPrevious(){
		return previous;
	}

	public void setCost(int c){
		cost = c;
	}

	public int CompareTo(ASquare compareASquare){
		return this.cost.CompareTo(compareASquare.cost);
	}

}
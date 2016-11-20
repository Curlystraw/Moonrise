﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/* Treat this script more as an example as to how Large-Menu scripts should be designed.
 * Keep in mind this script is a component of the panel labelled "Expand"
 * 
 * Note: You MUST put everything in OnEnable. Unity throws unfixable errors when
 * manipulating objects that aren't enabled for some reason. Some sort of 
 * SUCCEEDED(hr) that crashes the script object. 
 */

public class MapLoader : MonoBehaviour
{
    public Canvas canvas; //No idea how important this is. Probably only important for automated UI Movements
    RectTransform rtf;
    public GameObject gameManager;


    private Texture2D mapTex;
    private Texture2D miniTex;
    private Sprite mapSprite;
    private Sprite miniSprite;
    private BoardManager board;

    private GameObject miniMap;
    private GameObject mainMap;

    private int mapRadius = 7;
    // Use this for initialization. Start or Awake will crash it. 
    void Start()
    {
        miniMap = gameObject.transform.FindChild("Contents").gameObject;
        mainMap = gameObject.transform.FindChild("Expand").gameObject;
        board = (BoardManager)gameManager.GetComponent(typeof(BoardManager));
        loadMap();
        constructMiniMap();
    }

    /// <summary>
    /// Construct the most basic map.
    /// </summary>
    private void loadMap()
    {
        int[,] mapBoard = createMapBoard();

        mapTex = new Texture2D(mapBoard.GetLength(0)+2, mapBoard.GetLength(1)+2, TextureFormat.ARGB32, false);
        mapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < mapTex.width ; i++)
        {
            for (int j = 0; j < mapTex.height; j++)
            {
                mapTex.SetPixel(i, j, Color.black);
            }
        }

        for (int i = 1; i < mapTex.width-1;i++)
        {
            for(int j = 1; j < mapTex.height-1;j++)
            {
                if (mapBoard[i-1, j-1] == 0)
                    mapTex.SetPixel(i, j, Color.white);
            }
        }
        mapTex.Apply();
        mapSprite = Sprite.Create(mapTex, new Rect(0, 0, mapTex.width, mapTex.height), new Vector2(0.5f, 0.5f),16.0f);
        mainMap.transform.FindChild("MapStore").gameObject.GetComponent<Image>().sprite = mapSprite;
    }
    
    /// <summary>
    /// Update map every so often to reflect changes.
    /// </summary>
    private void updateMap()
    { 
        int[,] mapBoard = createMapBoard();
        GameObject[,] fog = createFogTile();
        Vector2 playerPos = getPlayerPosition();
        List<Vector2> enemies = GetEnemyPositions();

        //Passes to adjust the map
        mapBoard[(int)playerPos.x, (int)playerPos.y] = 3; //Player Pass
        foreach(Vector2 v in enemies) //Enemy Pass
        {
            mapBoard[(int)v.x, (int)v.y] = 2;
        }

        for (int i = 1; i < mapTex.width - 1; i++) //Raw Pass
        {
            for (int j = 1; j < mapTex.height - 1; j++)
            {
                if (mapBoard[i - 1, j - 1] == 3)           
                    mapTex.SetPixel(i, j, Color.green);
                else if (mapBoard[i - 1, j - 1] == 2)
                    mapTex.SetPixel(i, j, Color.red);
                else if (mapBoard[i - 1, j - 1] == 0)
                    mapTex.SetPixel(i, j, Color.white);
                else
                    mapTex.SetPixel(i, j, Color.gray);

                var curFog = fog[i, j].GetComponent<SpriteRenderer>(); //Fog Pass
                if (curFog != null)
                {
                    if (curFog.color.a > 0.8f) //Hidden and unseen
                    {
                        mapTex.SetPixel(i, j, Color.black);
                    }
                    else if (curFog.color.a > 0.5f && curFog.color.a < 0.8f) //Seen, but currently hidden
                    {
                        if (mapBoard[i - 1, j - 1] == 2)
                            mapTex.SetPixel(i, j, Color.white);

                        mapTex.SetPixel(i, j, mapTex.GetPixel(i, j) - new Color(0.2f, 0.2f, 0.2f, 0));
                    }

                }
            }
        }
        mapTex.Apply();
    }

    /// <summary>
    /// Start construction of a minimap
    /// </summary>
    private void constructMiniMap()
    {
        miniTex = new Texture2D(2*mapRadius+1,2*mapRadius+1, TextureFormat.ARGB32, false);
        miniTex.filterMode = FilterMode.Point;


        miniSprite = Sprite.Create(miniTex, new Rect(0, 0, miniTex.width, miniTex.height), new Vector2(0.5f, 0.5f), 16.0f);
        miniMap.transform.FindChild("MapStore").gameObject.GetComponent<Image>().sprite = miniSprite;
    }  
     
    /// <summary>
    /// Update the minimap
    /// </summary>
    private void UpdateMiniMap()
    {
        Vector2 playerPos = getPlayerPosition();
        int[,] mapBoard = createMapBoard();
        List<Vector2> enemies = GetEnemyPositions();
        GameObject[,] fog = createFogTile();

        mapBoard[(int)playerPos.x, (int)playerPos.y] = 3; //Player Pass
        foreach (Vector2 v in enemies) //Enemy Pass
        {
            mapBoard[(int)v.x, (int)v.y] = 2;
        }

        for (int i = -mapRadius; i <= mapRadius; i++) //Raw Pass
        {
            for (int j = -mapRadius; j <= mapRadius; j++)
            {
                if (playerPos.x + i < 0 || playerPos.x + i >= mapBoard.GetLength(0) || playerPos.y + j < 0 || playerPos.y + j >= mapBoard.GetLength(1))
                {
                    miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.black);
                }
                else
                {
                    if (mapBoard[(int)playerPos.x + i, (int)playerPos.y + j] == 3)
                        miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.green);
                    else if (mapBoard[(int)playerPos.x + i, (int)playerPos.y + j] == 2)
                        miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.red);
                    else if (mapBoard[(int)playerPos.x + i, (int)playerPos.y + j] == 0)
                        miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.white);
                    else
                        miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.gray);

                    var curFog = fog[(int)playerPos.x + i + 1, (int)playerPos.y + j + 1].GetComponent<SpriteRenderer>(); //Fog Pass
                    if (curFog != null)
                    {
                        if (curFog.color.a > 0.8f) //Hidden and unseen
                        {
                            miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.black);
                        }
                        else if (curFog.color.a > 0.5f && curFog.color.a < 0.8f) //Seen, but currently hidden
                        {
                            if (mapBoard[(int)playerPos.x + i, (int)playerPos.y + j] == 2)
                                miniTex.SetPixel(i + mapRadius, j + mapRadius, Color.white);

                            miniTex.SetPixel(i + mapRadius, j + mapRadius, miniTex.GetPixel(i + mapRadius, j + mapRadius) - new Color(0.2f, 0.2f, 0.2f, 0));
                        }

                    }
                }
            }
        }
        miniTex.Apply();
    }

    /// <summary>
    /// Creates a copy of the board from boardManager
    /// </summary>
    /// <returns></returns>
    private int[,] createMapBoard()
    {
        int[,] curBoard = board.getBoard(); //Code gymnastics is fun.
        return curBoard;
    }

    /// <summary>
    /// Creates a copy of the fog matrix from BoardManager
    /// </summary>
    /// <returns></returns>
    private GameObject[,] createFogTile()
    {
        GameObject[,] fogTiles = board.getFogTiles();
        return fogTiles;
    }

    /// <summary>
    /// Grabs the player position. Done here since the player object in BoardManager is weird
    /// </summary>
    /// <returns></returns>
    private Vector2 getPlayerPosition()
    {
        Vector2 position;
        Transform player = GameObject.Find("Player").transform;
        position = new Vector2(player.position.x, player.position.y);
        return position;
    }

    /// <summary>
    /// Grabs the positions of all enemies
    /// </summary>
    /// <returns></returns>
    public List<Vector2> GetEnemyPositions()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        List<Vector2> returnList = new List<Vector2>();
       foreach (GameObject t in enemyList)
        {
            Vector2 pos = new Vector2(t.transform.position.x, t.transform.position.y);
            returnList.Add(pos);
        }
        return returnList;
    }

    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            //If the UI element needs to repeatedly update: Do it here.
            updateMap();
            UpdateMiniMap();
        }
    }

}

using UnityEngine;
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
    private Sprite mapSprite;
    private BoardManager board;
    // Use this for initialization. Start or Awake will crash it. 
    void OnEnable()
    {
        Center(); // While not important to be first, it's best if it is. Centering the window correctly takes little effort and prevents the menu from never appearing due to shenanigans.
    }
    void Awake()
    {
        board = (BoardManager)gameManager.GetComponent(typeof(BoardManager));
        loadMap();
    }

    /// <summary>
    ///  Force Centers the UI element. 
    /// </summary>
    private void Center() //Private because nothing outside this script should be using this.
    {
        rtf = gameObject.GetComponent<RectTransform>();
        
        int width = (int)rtf.rect.width;
        int height = (int)rtf.rect.height;
        float x = (Screen.width) / 2; //Anchor of Panel SHOULD be centered. No need to worry.
        float y = (Screen.height) / 2;
        Vector3 newpos = new Vector3(x, y);
        rtf.position = newpos;
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
        gameObject.transform.FindChild("MapStore").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(mapBoard.GetLength(0)*10,mapBoard.GetLength(1)*10);
        gameObject.transform.FindChild("MapStore").gameObject.GetComponent<Image>().sprite = mapSprite;
    }
    
    /// <summary>
    /// Update map every so often to reflect changes.
    /// </summary>
    private void updateMap()
    { 
        int[,] mapBoard = createMapBoard();
        GameObject[,] fog = createFogTile();
        Vector2 playerPos = getPlayerPosition();
        List<Vector2> enemies = board.GetEnemyPositions();

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

            }
        }
        for (int i = 1; i < mapTex.width - 1; i++) //Fog Pass
        {
            for (int j = 1; j < mapTex.height - 1; j++)
            {
                var curFog = fog[i, j].GetComponent<SpriteRenderer>();
                if (curFog != null)
                {
                    if (curFog.color.a > 0.8f) //Deep Fog
                    {
                        mapTex.SetPixel(i, j, Color.black);
                    }
                    else if (curFog.color.a > 0.5f && curFog.color.a < 0.8f) //Seen, but currently hidden
                    {
                        if (mapBoard[i - 1, j - 1] == 2)
                            mapTex.SetPixel(i, j, Color.white);
                    }
                    
                }

            }
        }
        mapTex.Apply();
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


    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            //If the UI element needs to repeatedly update: Do it here.
            updateMap();
        }
    }

}

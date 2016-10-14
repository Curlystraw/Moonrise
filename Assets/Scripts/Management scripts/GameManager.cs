using UnityEngine;
using System.IO;
using System.Collections;

namespace Completed
{
    using System.Collections.Generic;       
    using UnityEngine.UI;                   

    public class GameManager : MonoBehaviour
    {

        public float levelStartDelay = 2f;                      
        public float turnDelay = 0.1f;                          
        public int playerGoldPoints = 100;                      
        public static GameManager instance = null;              
        [HideInInspector]
        public bool playersTurn = true;       


        private Text levelText;                                 
        private GameObject levelImage;                        
        private BoardManager boardScript;                       
        private int level = 1;                                  
        private List<Enemy> enemies;                          
        private bool enemiesMoving;                             
        private bool doingSetup = true;     
                            


        /// <summary>
        /// Pre-Start initialization
        /// </summary>
        void Awake()
        {
            if (instance == null)

                instance = this;
            else if (instance != this)

                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            enemies = new List<Enemy>();

            boardScript = GetComponent<BoardManager>();

            InitGame();
        }

        /*void OnLevelWasLoaded(int index)
        {
            level++;
            InitGame();
        }*/

		/// <summary>
		/// Displays day, begins board setup
		/// </summary>
        void InitGame()
        {
            //Retrieve encounters
            string[] files = null;

			files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string fileName in files)
            {
                Debug.Log(fileName);
            }

            doingSetup = true;

            levelImage = GameObject.Find("LevelImage");

            levelText = GameObject.Find("LevelText").GetComponent<Text>();

            levelText.text = "Day " + level;

            levelImage.SetActive(true);

            Invoke("HideLevelImage", levelStartDelay);

            enemies.Clear();

            boardScript.SetupScene(level);

        }


        void HideLevelImage()
        {
            levelImage.SetActive(false);

            doingSetup = false;
        }

        void Update()
        {
            if (playersTurn || enemiesMoving || doingSetup)

                return;
			
			StartCoroutine(MoveEnemies());
        }

        public void AddEnemyToList(Enemy script)
        {
            enemies.Add(script);
        }


        public void GameOver()
        {
            levelText.text = "After " + level + " days, you died.";

            levelImage.SetActive(true);

            enabled = false;
        }

		/// <summary>
		/// Processes enemy turns
		/// </summary>
        IEnumerator MoveEnemies()
        {
			bool init = true;		//On the first rotation, the enemy adds their speed to their action points
			bool moreMoves = true;	//If there is an enemy who can still take an action, enemy turn does not end
            enemiesMoving = true;

            yield return new WaitForSeconds(turnDelay*2);

            if (enemies.Count == 0)
            {
                yield return new WaitForSeconds(turnDelay);
            }

			//As long as an enemy can move, the enemy turn continues
			while(moreMoves){
				moreMoves = false;
				for (int i = 0; i < enemies.Count; i++)
				{
					bool canMove = enemies[i].takeTurn(init);
					
					moreMoves |= canMove;	//Logical OR, if any enemy returns true, moreMoves will equal true
					
					
				}
				yield return new WaitForSeconds(enemies[0].moveTime+0.05f);
				init = false;		//Make sure the enemies don't get speed added each time
			}
			
            playersTurn = true;

            enemiesMoving = false;
        }
    }
}


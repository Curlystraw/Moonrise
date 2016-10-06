using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Completed
{

    public class Player : MovingObject
    {
		public GameManager game = GameManager.instance;
		
        public int wallDamage = 1;
        public int pointsPerGold = 10;
        public float restartLevelDelay = 1f;
        public Text goldText;
		public Text timeLeft;

        private Animator animator;

        // Use this for initialization
        protected override void Start()
        {
			speed = 1;
		
            animator = GetComponent<Animator>();

            base.Start();
        }

        private void OnDisable()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.playersTurn) return;

            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove<Wall>(horizontal, vertical);
            }
        }

        protected override void AttemptMove<T>(int xDir, int yDir)
        {

			GameManager.instance.timeLeft--;
			timeLeft.text = "TimeLeft: " + GameManager.instance.timeLeft;

            base.AttemptMove<T>(xDir, yDir);

            RaycastHit2D hit;

            CheckIfGameOver();

            GameManager.instance.playersTurn = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
			//Debug.Log("#TRIGGERED");
            if (other.tag == "Exit")
            {
                Invoke("Restart", restartLevelDelay);
                enabled = false;
            }
            else if (other.tag == "Gold")
            {
                gold += pointsPerGold;
                goldText.text = "+" + pointsPerGold + " Gold";
                other.gameObject.SetActive(false);
            }
        }

        protected override void OnCantMove<T>(T component)
        {
            Wall hitWall = component as Wall;

        }

        private void Restart()
        {
            Application.LoadLevel(Application.loadedLevel);
        }

		public void LoseHp(int loss)
		{
			GameManager.instance
		}

        public void LoseGold(int loss)
        {
            gold -= loss;
            goldText.text = "-" + loss + " Gold";
            CheckIfGameOver();
        }

        private void CheckIfGameOver()
        {
            if (gold <= 0)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}

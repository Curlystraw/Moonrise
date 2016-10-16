using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Completed
{

    public class Player : MovingObject
    {
	    public int wallDamage = 1;
        public int pointsPerGold = 10;
        public float restartLevelDelay = 1f;

		public Text displayText;
		public Text actionText;

		public String timeLeft;
		public String goldText;
		public String hpText;

        private Animator animator;

        // Use this for initialization
        protected override void Start()
        {
			speed = 1;

			timeLeft = "Time Left: " + GameManager.instance.timeLeft;
			goldText = "Gold: " + GameManager.instance.playerGoldPoints;
			hpText = "HP: " + GameManager.instance.playerHp;
			UpdateText ();
		
            animator = GetComponent<Animator>();

            base.Start();
        }

        private void OnDisable()
        {
        }

		public void UpdateText(String message = "")
		{
			displayText.text = timeLeft + " | " + goldText + " | " + hpText;
			if (message != "") {
				displayText.text += " | " + message;
			}
		}

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.playersTurn) return;

            int horizontal = 0;
            int vertical = 0;

			if (Input.GetMouseButtonDown(0)) {
				if (GameManager.instance.enemyClicked) {
					RangedAttack ();
				}

				// Add other things like "interact" or "talk" here
			} else {
				horizontal = (int)Input.GetAxisRaw ("Horizontal");
				vertical = (int)Input.GetAxisRaw ("Vertical");

				if (horizontal != 0)
					vertical = 0;

				if (horizontal != 0 || vertical != 0) {
					AttemptMove<Wall> (horizontal, vertical);
				}
			}
        }

		protected void RangedAttack()
		{
			actionText.text = "You attacked an enemy!";
			GameManager.instance.enemyClicked = false;

			EndTurn ();
		}

		protected void EndTurn()
		{
			GameManager.instance.timeLeft--;
			timeLeft = "Time Left: " + GameManager.instance.timeLeft;
			UpdateText ();

			CheckIfGameOver();

			GameManager.instance.playersTurn = false;
		}

        protected override void AttemptMove<T>(int xDir, int yDir)
        {
			base.AttemptMove<T>(xDir, yDir);

            RaycastHit2D hit;

			EndTurn ();
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
				GameManager.instance.playerGoldPoints += pointsPerGold;
				goldText = "Gold: " + GameManager.instance.playerGoldPoints;
				String message = "+" + pointsPerGold + " Gold";
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

		public override void LoseHp(int loss)
		{
			GameManager.instance.playerHp -= loss;
			String message = "-" + loss + " HP";
			hpText = "HP: " + GameManager.instance.playerHp;
			UpdateText ();
			CheckIfGameOver();
		}

        public void LoseGold(int loss)
        {
			GameManager.instance.playerGoldPoints -= loss;
			String message = "-" + loss + " Gold";
			goldText = "Gold: " + GameManager.instance.playerGoldPoints;
			UpdateText (message);
        }

        private void CheckIfGameOver()
        {
			if (GameManager.instance.playerGoldPoints <= 0 || GameManager.instance.playerHp <= 0)
            {
                GameManager.instance.GameOver();
            }
        }
			
		protected override void KillObject()
		{
			return;
		}
    }
}

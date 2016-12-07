using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Completed
{

	public class Player : Character
    {
        public int wallDamage = 1;
        public int pointsPerGold = 10;
        public float restartLevelDelay = 1f;
		public int sneak = 4;
		public float sightRange = 12f;
		public Sprite werewolfFront;
		public Sprite werewolfBack;
		public Sprite werewolfLeft;
		public Sprite werewolfRight;
		public Sprite humanFront;
		public Sprite humanBack;
		public Sprite humanLeft;
		public Sprite humanRight;
		public Color original;
		public GameObject indicator;

		public Text displayText;
		public String timeLeft;
		public String goldText;
		public String hpText;
		public Text actionText;

		private BoxCollider2D hitbox;	//hitbox for the object - used for raycast tests?
        private Animator animator;
		public LayerMask sightBlocks, fogLayer;
		private ArrayList revealed;

		//Healthbar object
		public GameObject hpBar;

        // Use this for initialization
        protected override void Start()
        {

			speed = 1;
			orientation = Orientation.North;
			original = this.gameObject.GetComponent<SpriteRenderer> ().color;
			timeLeft = "Time Left: " + GameManager.instance.timeLeft;
			goldText = "Gold: " + GameManager.instance.playerGoldPoints;
			hpText = "HP: " + this.CurrentHP;
			UpdateText ();
		
			animator = GetComponent<Animator>();
			hitbox = GetComponent<BoxCollider2D>();


			//sightBlocks = LayerMask.NameToLayer("BlockingLayer");
			//fogLayer = LayerMask.NameToLayer("Fog");
			revealed = new ArrayList();

            base.Start();
			OnFinishMove();
        }

        private void OnDisable()
        {
        }

		public void UpdateText(String message = "")
		{
			displayText.text = timeLeft + " | " + goldText + " | " + hpText;
			Vector3 scale = hpBar.transform.localScale;
			scale.x = ((float)currentHP/(float)totalHP);
			hpBar.transform.localScale = scale;

			if (message != "") {
				displayText.text += " | " + message;
			}
		}

		protected override void UpdateSprite()
		{
			Sprite sprite;
			Color color;
			if(GameManager.instance.isWerewolf) {
				if (orientation == Orientation.North)
					sprite = werewolfBack;
				else if (orientation == Orientation.East)
					sprite = werewolfRight;
				else if (orientation == Orientation.South)
					sprite = werewolfFront;
				else
					sprite = werewolfLeft;
				color = Color.gray;
			} else {
				if (orientation == Orientation.North)
					sprite = humanBack;
				else if (orientation == Orientation.East)
					sprite = humanRight;
				else if (orientation == Orientation.South)
					sprite = humanFront;
				else
					sprite = humanLeft;
				color = original;
			}
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = sprite;
			this.gameObject.GetComponent<SpriteRenderer> ().color = color;
		}

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.playersTurn) return;

			if (Input.GetKeyDown (KeyCode.T)) {
				actionText.text = "";
				switchForm ();
				GameManager.instance.playersTurn = false;
				GameManager.instance.timeLeft--;
				timeLeft = "Time Left: " + GameManager.instance.timeLeft;
				UpdateText ();
				return;
			} else if (Input.GetMouseButtonDown (0)) {
				if (GameManager.instance.enemyClicked) {
					Attack ();
				}
			}
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");
			bool spacebar = Input.GetKeyUp(KeyCode.Space);

            if (horizontal != 0 || vertical != 0 || spacebar)
			{
				actionText.text = "";
                AttemptMove(horizontal, vertical);
            }
        }

		protected bool WillHitWall(int xDir, int yDir, out RaycastHit2D hit)
		{
			//Find movement points
			Vector2 start = transform.position;
			Vector2 end = start + new Vector2(xDir, yDir);

			//BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();



			//Check if the move isn't blocked
			//boxCollider.enabled = false;
			hit = Physics2D.Linecast(end, start, blockingLayer);
			//boxCollider.enabled = true;


			if (hit.transform != null) {
				if (hit.transform.tag == "Wall") {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

		protected override void AttemptMove(int xDir, int yDir)
		{
			base.AttemptMove (xDir, yDir);

			RaycastHit2D hit;
			bool canMove = Move (xDir, yDir, out hit);

			bool willHitWall = WillHitWall (xDir, yDir, out hit);

			// Only reset turn if can move
			if (!willHitWall) {
				GameManager.instance.timeLeft--;
				timeLeft = "Time Left: " + GameManager.instance.timeLeft;
				UpdateText ();

				CheckIfGameOver ();

				GameManager.instance.playersTurn = false;
			}

			if (hit.transform != null && !canMove)
				OnCantMove (hit.transform);
        }

		protected void Attack()
		{
			actionText.text += "You attacked an enemy!\n";
			GameManager.instance.enemyClicked = false;

			EndTurn ();
		}

		protected void EndTurn()
		{
			GameManager.instance.timeLeft--;
			timeLeft = "Time Left: " + GameManager.instance.timeLeft;
			UpdateText ();

			CheckIfGameOver ();
			GameManager.instance.playersTurn = false;
		}

        //pick up an item. If nothing on square does nothing.
        private void OnTriggerEnter2D(Collider2D other)
        {
			//Debug.Log("#TRIGGERED");
            if (other.tag == "Exit")
            {
                Invoke("Restart", restartLevelDelay);
                enabled = false;
            }
            else if (other.tag == "Item")
            {
				int chance = UnityEngine.Random.Range (0, 2);
				String message = "";
				switch (chance) {
				case 0:
					GameManager.instance.print("Picked up "+pointsPerGold+" gold");
					GameManager.instance.playerGoldPoints += pointsPerGold;
					goldText = "Gold: " + GameManager.instance.playerGoldPoints;
					message = "+" + pointsPerGold + " Gold";
					UpdateText ();
					other.gameObject.SetActive(false);
					break;
				case 1:
					LoseHp (-20);
					message = "+" + 20 + " Health";
					UpdateText ();
					other.gameObject.SetActive(false);
					break;
				default:
					GameManager.instance.print("Picked up "+pointsPerGold+" gold");
					GameManager.instance.playerGoldPoints += pointsPerGold;
					goldText = "Gold: " + GameManager.instance.playerGoldPoints;
					message = "+" + pointsPerGold + " Gold";
					UpdateText ();
					other.gameObject.SetActive(false);
					break;
				}

            }
        }

        private void interact()
        {

        }

        protected override void OnCantMove(Transform transform)
        {
			Character character = transform.GetComponent<Character> ();
			if (character is Enemy) {
				
			} else if (character is Chest) {
				Chest chest = (Chest)character;
				chest.ObtainItem (this);
			}
        }

        private void Restart()
        {
           Application.LoadLevel(Application.loadedLevel);
			//SceneManager.LoadScene(SceneManager.GetActiveScene);
        }

		public void LoseHp(int loss)
		{
			this.CurrentHP -= loss;
			if (this.currentHP < 0) {
				this.currentHP = 0;
			} else if (this.currentHP > 100) {
				this.currentHP = 100;
			}
			/*String message;
			if (loss > 0) {
				message = "-" + loss + " HP";
			} else {
				message = "+" + loss + " HP";
			}*/
			hpText = "HP: " + this.CurrentHP;
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
			if (GameManager.instance.playerGoldPoints <= 0 || this.CurrentHP <= 0)
            {
                GameManager.instance.GameOver();
            }
        }

		protected override void OnFinishMove ()
		{
			//StartCoroutine(FogCheck());
			FogCheck();
		}

		//Casts 17 rays, then casts a line along that detected ray to identify which squares are visible.
		public void FogCheck(){
			float angle = 0;
			Vector3 pPos = this.transform.position;
			Vector2 origin = new Vector2(pPos.x,pPos.y), direction,end;
			RaycastHit2D[] fogHits;
			ArrayList fog = new ArrayList();
			while(angle <= Math.PI*2+0.01){//Added small value to account for float point error
				direction = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
				hitbox.enabled = false;
				RaycastHit2D hit;
				hit = Physics2D.Raycast(origin,direction,sightRange,sightBlocks);
				if(hit.distance > 0){
					end = hit.point;
				}
				else{
					end = origin+(direction*sightRange);
					//Debug.Log(direction*sightRange);
				}
				//Instantiate(indicator,end,Quaternion.identity);
				fogHits = Physics2D.LinecastAll(origin,end,fogLayer);
				hitbox.enabled = true;
				foreach(RaycastHit2D f in fogHits){
					f.collider.GetComponent<FogOfWar>().isVisible(true);
					if(fog.IndexOf(f.collider.GetComponent<FogOfWar>()) < 0)
						fog.Add(f.collider.GetComponent<FogOfWar>());
				}
				//Debug.Log(direction);

				angle += (float)(Math.PI/18f);
				//yield return null;
			}

			//Debug.Log(revealed.Count);
			foreach(FogOfWar f in revealed){
				int ind = fog.IndexOf(f);
				if(fog.IndexOf(f) < 0){
					f.isVisible(false);
				}
			}

			revealed = fog;
		}


		//Switchs form (human or werewolf); updates hp and sprite
		private void switchForm () {
			GameManager.instance.isWerewolf = !GameManager.instance.isWerewolf;
			if (GameManager.instance.isWerewolf) {
				this.TotalHP *= 2;
				this.CurrentHP *= 2;
			} else {
				this.TotalHP /= 2;
				this.CurrentHP /= 2;
			}
			hpText = "HP: " + this.CurrentHP;
			UpdateText ();
			UpdateSprite ();
		}
	}

	public enum Orientation
	{
		North,
		East,
		South,
		West
	}
}

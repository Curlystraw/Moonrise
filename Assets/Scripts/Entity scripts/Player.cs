using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Completed
{

    public class Player : MovingObject
    {
		public int sneak = 4;
        public int wallDamage = 1;
        public int pointsPerGold = 10;
        public float restartLevelDelay = 1f;
		public float sightRange = 12f;
		public Sprite werewolfSprite;
		public Sprite humanSprite;
		public Color original;
		public GameObject indicator;

		public Text displayText;
		public String timeLeft;
		public String goldText;
		public String hpText;

		private BoxCollider2D hitbox;	//hitbox for the object - used for raycast tests?
        private Animator animator;
		public LayerMask sightBlocks, fogLayer;
		private ArrayList revealed;

        // Use this for initialization
        protected override void Start()
        {
			speed = 1;
			original = this.gameObject.GetComponent<SpriteRenderer> ().color;
			timeLeft = "Time Left: " + GameManager.instance.timeLeft;
			goldText = "Gold: " + GameManager.instance.playerGoldPoints;
			hpText = "HP: " + GameManager.instance.playerHp;
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
			if (message != "") {
				displayText.text += " | " + message;
			}
		}

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.playersTurn) return;

			if (Input.GetKeyDown (KeyCode.T)) {
				switchForm ();
				GameManager.instance.playersTurn = false;
				GameManager.instance.timeLeft--;
				timeLeft = "Time Left: " + GameManager.instance.timeLeft;
				UpdateText ();
				return;
			}
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");
			bool spacebar = Input.GetKeyUp(KeyCode.Space);

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0 || spacebar)
            {
                AttemptMove<Wall>(horizontal, vertical);
            }
        }

		protected override void AttemptMove<T>(int xDir, int yDir)
        {

			GameManager.instance.timeLeft--;
			timeLeft = "Time Left: " + GameManager.instance.timeLeft;
			UpdateText ();

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

		public void LoseHp(int loss)
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

		protected override void OnFinishMove ()
		{
			//StartCoroutine(FogCheck());
			FogCheck();
		}

		//Casts 17 rays, then casts a line along that detected ray to identify which squares are visible.
		protected void FogCheck(){
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

				angle += (float)(Math.PI/16f);
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
				GameManager.instance.playerHp *= 2;
				hpText = "HP: " + GameManager.instance.playerHp;
				UpdateText ();
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = werewolfSprite;
				this.gameObject.GetComponent<SpriteRenderer> ().color = Color.gray;
			} else {
				GameManager.instance.playerHp /= 2;
				hpText = "HP: " + GameManager.instance.playerHp;
				UpdateText ();
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = humanSprite;
				this.gameObject.GetComponent<SpriteRenderer> ().color = original;
			}
				
		}
    }

}

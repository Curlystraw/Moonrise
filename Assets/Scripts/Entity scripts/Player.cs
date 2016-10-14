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
        public Text goldText;
		public float sightRange = 12f;
		public GameObject indicator;

		private BoxCollider2D hitbox;	//hitbox for the object - used for raycast tests?
        private Animator animator;
        private int gold;
		public LayerMask sightBlocks, fogLayer;
		private ArrayList revealed;

        // Use this for initialization
        protected override void Start()
        {
			speed = 1;
		
			animator = GetComponent<Animator>();
			hitbox = GetComponent<BoxCollider2D>();

            gold = GameManager.instance.playerGoldPoints;

			//sightBlocks = LayerMask.NameToLayer("BlockingLayer");
			//fogLayer = LayerMask.NameToLayer("Fog");
			revealed = new ArrayList();

            base.Start();
			OnFinishMove();
        }

        private void OnDisable()
        {
            GameManager.instance.playerGoldPoints = gold;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance.playersTurn) return;

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

            gold--;
            goldText.text = "Gold: " + gold;

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
    }
}

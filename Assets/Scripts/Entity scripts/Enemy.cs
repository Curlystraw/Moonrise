using UnityEngine;
using System.Collections.Generic;

namespace Completed
{
	/// <summary>
	/// Generic enemy class, contains a melee attack and line-of-sight based aggression.
	/// </summary>
	public class Enemy : Character
    {

        public int playerDamage;
		public GameObject indicator;
		public float sightRange;

        private Animator animator;
        private Transform target;
		private Player player;
		private BoardManager board;
		private Vector2 targetLoc;
        private bool skipMove, visible;
		private List<Vector2> path;


		//Contains enemy's hitbox
		private BoxCollider2D hitbox;

        protected override void Start()
        {
            GameManager.instance.AddEnemyToList(this);

            animator = GetComponent<Animator>();
			hitbox = GetComponent<BoxCollider2D>();

            target = GameObject.FindGameObjectWithTag("Player").transform;
			player = target.GetComponent<Player>();
			board = GameObject.FindGameObjectWithTag("GameController").GetComponent<BoardManager>();

			targetLoc = new Vector2(-1	,-1);
            base.Start();

			path = new List<Vector2>();
        }

		protected void Update(){
			//checkVisible();

			Color c = this.GetComponent<SpriteRenderer>().color;
			if(c.a > 0 && !visible)
				c.a -= 0.05f;
			else if(c.a < 1 && visible)
				c.a += 0.05f;
			this.GetComponent<SpriteRenderer>().color = c;
		}

        /*protected override void AttemptMove<T>(int xDir, int yDir)
        {
            
            base.AttemptMove<T>(xDir, yDir);

            skipMove = true;
        }*/

		/// <summary>
		///	This is where the enemy logic goes, for the base class should be a basic line of sight = attack processing.
		/// </summary>
        public bool takeTurn(bool init)
        {
			RaycastHit2D hit;
			hitbox.enabled = false;
			hit = Physics2D.Linecast(new Vector2(transform.position.x,transform.position.y),new Vector2(target.position.x,target.position.y), blockingLayer);
			hitbox.enabled = true;
			float range = sightRange-player.sneak;
			if(hit.transform == target && hit.distance <= range){
				targetLoc = new Vector2(target.position.x,target.position.y);
				path = board.findPath(new Vector2((int)transform.position.x,(int)transform.position.y),targetLoc);
				Debug.Log("\"Target sighted!\" - "+this.name.ToString());
				/*for(int i = 0; i < path.Count; i++){
					Vector2 targ = path[i];
					Instantiate(indicator, new Vector2(targ.x,targ.y), Quaternion.identity);
				}*/
				//Debug.Log(targetLoc);
			}

			if(init)
				AP += speed;
				
			if(AP < 1)
				return false;
				
			
			int xDir = 0;
			int yDir = 0;

			//Attempt to pursue target
			if(path.Count > 0){
				yDir = 0;
				xDir = 0;
				//Debug.Log(path[0]+" "+path[path.Count-1]);
				while(Mathf.Abs(yDir)+Mathf.Abs(xDir) < float.Epsilon){
					yDir = (int)(path[path.Count-1].y-transform.position.y);
					xDir = (int)(path[path.Count-1].x-transform.position.x);
					if(Mathf.Abs(yDir)+Mathf.Abs(xDir) < float.Epsilon){
						path.RemoveAt(path.Count-1);

						if(path.Count == 0){
							Debug.Log("\"Target Lost.\" - "+this.name.ToString());
							break;
						}
					}
				}
				/*if (Mathf.Abs(targetLoc.y - transform.position.y) > float.Epsilon)
					yDir = targetLoc.y > transform.position.y ? 1 : -1;

				else if (Mathf.Abs(targetLoc.x - transform.position.x) > float.Epsilon)
					xDir = targetLoc.x > transform.position.x ? 1 : -1;
				else
					targetLoc = new Vector2();*/ 	
				
			}
			//If no target is known, move randomly
			else{
				int moveType = Mathf.FloorToInt(Random.Range(0,5));
				switch(moveType){
				case 0:
					break;
				case 1:
					xDir = 1;
					break;
				case 2:
					xDir = -1;
					break;
				case 3:
					yDir = 1;
					break;
				case 4:
					yDir = -1;
					break;
				}
			}

			AttemptMove<Player>(xDir, yDir);
			AP--;
			
			//Return true if the enemy can move again
			return (AP >= 1);
        }


        protected override void OnCantMove<T>(T component)
        {
            Player hitPlayer = component as Player;

            hitPlayer.LoseHp(playerDamage);
        }

		protected override void OnFinishMove ()
		{
			

		}

		private void checkVisible(){
			if(DistToTarget() > 6)
				return;
			RaycastHit2D hit;
			hitbox.enabled = false;
			hit = Physics2D.Linecast(new Vector2(transform.position.x,transform.position.y),new Vector2(target.position.x,target.position.y), blockingLayer);
			hitbox.enabled = true;

			if(hit != null){
				if(hit.distance > 0 && hit.collider.gameObject.tag == "Player"){
					//c.a = 1;
					//this.GetComponent<SpriteRenderer>().color = c;
					visible = true;
				}
				else{
					visible = false;
					//c.a = 0;
					//this.GetComponent<SpriteRenderer>().color = c;
				}
			}
		}


		private float DistToTarget(){
			return Mathf.Pow(Mathf.Pow(transform.position.x-target.position.x,2f)+Mathf.Pow(transform.position.y-target.position.y,2f),0.5f);
		}

		public void isVisible(bool v){
			visible = v;
		}
    }
}

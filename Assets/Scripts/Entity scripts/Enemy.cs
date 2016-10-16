using UnityEngine;
using System.Collections;

namespace Completed
{
    public class Enemy : MovingObject
    {

        public int playerDamage;

        private Animator animator;
        private Transform target;
        private bool skipMove;

        protected override void Start()
        {
			hp = 100;

            GameManager.instance.AddEnemyToList(this);

            animator = GetComponent<Animator>();

            target = GameObject.FindGameObjectWithTag("Player").transform;

            base.Start();
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
			
			if(init)
				AP += speed;
				
			if(AP < 1)
				return false;
				
			
			int xDir = 0;
			int yDir = 0;

			if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
				yDir = target.position.y > transform.position.y ? 1 : -1;

			else
				xDir = target.position.x > transform.position.x ? 1 : -1;

			AttemptMove<Player>(xDir, yDir);
			AP--;
			
			//Return true if the enemy can move again
			return (AP >= 1);
        }

		void OnMouseDown() {

			if (GameManager.instance.playersTurn) {
				GameManager.instance.enemyClicked = true;

				LoseHp (5);
			}

		}

        protected override void OnCantMove<T>(T component)
        {
            Player hitPlayer = component as Player;

            hitPlayer.LoseHp(playerDamage);
        }

		protected override void KillObject()
		{
			GameManager.instance.RemoveEnemyFromList (this);
			Destroy (gameObject);
		}
    }
}

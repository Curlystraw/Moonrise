using UnityEngine;
using System.Collections;


namespace Completed {
	public class FogOfWar : MonoBehaviour {
		private float FOGMAX = 1f, FOGMIN = 0.5f;

		private BoardManager b;

		//Is this tile visible?
		private bool visible, oldVis;
		//Has this tile been uncovered before?
		private bool seen;
		// Use this for initialization
		void Start () {
			visible = false;
			oldVis = false;
			//Get board manager
			b = GameObject.Find("GameManager").GetComponent<BoardManager>();
		}
		
		// Update is called once per frame
		void Update () {
			SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
			//Fade the fog in or out as needed
			float alpha = sp.color.a;
			if(!visible && !seen && alpha < FOGMAX){
				alpha += 0.05f;
			}
			else if(!visible && seen && alpha < FOGMIN){
				alpha += 0.05f;
			}
			else if(visible && alpha > 0){
				alpha -= 0.05f;
			}
			//Values of the color object cannot be modified with the return type, temporary variable workaround
			Color c = sp.color;
			c.a = alpha;
			sp.color = c;

			//if(visible != oldVis){
				GameObject enemyT = b.getEnemy(new Vector2(this.transform.position.x,this.transform.position.y));
				if(enemyT != null)
					enemyT.GetComponent<Enemy>().isVisible(visible);
				oldVis = visible;
			//}
		}

		public void isVisible(bool vis){
			visible = vis;
			if(vis)
				seen = true;
		}

		public bool isVisible(){
			return visible;
		}
	}

}
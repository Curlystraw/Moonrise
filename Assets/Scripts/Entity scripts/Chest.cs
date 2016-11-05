using UnityEngine;
using System.Collections.Generic;
using ItemSpace;

namespace Completed {
	public class Chest : Character {
		private Transform playerTransform;
		private Player player;
		private Item item; //item contained in chest;
		// Use this for initialization
		void Start () {
			CurrentHP = 1;
			playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
			player = playerTransform.GetComponent<Player>();
			item = new Weapon ();
		}

	
		// Update is called once per frame
		void Update () {
			// if(hp == 0) {
			if (Mathf.Sqrt(Mathf.Pow(playerTransform.position.x - this.transform.position.x, 2) + Mathf.Pow(playerTransform.position.y - this.transform.position.y, 2)) == 1) {
				player.AddItem (item);
				print ("A " + item.Name + " was added to inventory");
				Destroy (this.gameObject);
			}
	
		}
	}
}
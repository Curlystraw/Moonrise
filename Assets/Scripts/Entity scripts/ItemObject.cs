using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Completed;

namespace ItemSpace {
	public class ItemObject : MonoBehaviour
	{
		// prefab
		public Texture crossbowTexture;
		public Texture talismanTexture;
		public Texture otherTexture;

		private Item item;
		private RawImage image;

		void OnMouseDown()
		{
			GameManager.instance.print ("inventory item clicked");
		}

		public Item Item {
			get {
				return item;
			}
			set {
				item = value;
				Texture t;
				if (item is Weapon) {
					Weapon weapon = (Weapon)item;
					if (weapon.Type == WeaponType.Crossbow)
						t = crossbowTexture;
					else
						t = talismanTexture;
				} else {
					t = otherTexture;
				}
				image.texture = t;
			}
		}

		void Awake ()
		{
			// I have no idea why this needs to be in Awake() instead of Start() ...
			image = this.gameObject.GetComponent<RawImage> ();
		}

		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}

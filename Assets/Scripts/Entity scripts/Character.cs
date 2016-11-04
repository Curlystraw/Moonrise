using System;
using ItemSpace;

namespace Completed
{
	public class Character : MovingObject
	{
		//leveled up with magic character points
		private int baseHP;
		private int baseArmor;
		private int baseAttack;

		//affected by items
		private int totalHP;
		private int totalArmor;
		private int totalAttack;

		private int currentHP;

		private EquippedItemSet equippedItems;
		private Inventory inventory;

		public Character () : this(7, 2, 3)
		{
			currentHP = 6;
		}

		public Character (int hp, int armor, int attack)
		{
			baseHP = hp;
			baseArmor = armor;
			baseAttack = attack;

			currentHP = baseHP;

			totalHP = baseHP;
			totalArmor = baseArmor;
			totalAttack = baseAttack;

			equippedItems = new EquippedItemSet ();
			inventory = new Inventory ();
		}

		/// <summary>
		/// Add the item to the inventory.
		/// </summary>
		/// <param name="item">Item.</param>
		public void AddItem(Item item)
		{
			inventory.AddItem (item);
		}

		/// <summary>
		/// Equip the selected item from the inventory, 
		/// and remove the item from the inventory.
		/// If an item of the same type is already equipped, unequip it.
		/// </summary>
		/// <param name="item">Item.</param>
		public void EquipItem(Item item)
		{
			EquipItem equippable;
			if (item is EquipItem) {
				equippable = (EquipItem)item;
				if (RemoveItem (equippable)) {
					Item unequipped = equippedItems.Unequip (equippable.GetItemClass ());
					if (equippable != null)
						inventory.AddItem (unequipped);
					equippedItems.Equip (equippable);
				}
			}
		}

		/// <summary>
		/// Unequip the item and add it to the inventory.
		/// </summary>
		/// <param name="ic">Item class.</param>
		public void UnequipItem(ItemClass ic)
		{
			Item unequipped = equippedItems.Unequip (ic);
			AddItem (unequipped);
		}

		/// <summary>
		/// Remove the item from the inventory.
		/// </summary>
		/// <returns><c>true</c>, if item was removed, <c>false</c> otherwise.</returns>
		/// <param name="item">Item.</param>
		public bool RemoveItem(Item item)
		{
			return inventory.RemoveItem (item);
		}
			
		public int BaseHP {
			get {
				return this.baseHP;
			}
			set {
				baseHP = value;
			}
		}

		public int BaseArmor {
			get {
				return this.baseArmor;
			}
			set {
				baseArmor = value;
			}
		}

		public int BaseAttack {
			get {
				return this.baseAttack;
			}
			set {
				baseAttack = value;
			}
		}

		public int TotalHP {
			get {
				return this.totalHP;
			}
			set {
				totalHP = value;
			}
		}

		public int TotalArmor {
			get {
				return this.totalArmor;
			}
			set {
				totalArmor = value;
			}
		}

		public int TotalAttack {
			get {
				return this.totalAttack;
			}
			set {
				totalAttack = value;
			}
		}

		public int CurrentHP {
			get {
				return this.currentHP;
			}
			set {
				currentHP = value;
			}
		}

		public EquippedItemSet EquippedItems {
			get {
				return equippedItems;
			}
		}

		public Inventory Inventory {
			get {
				return inventory;
			}
			set {
				inventory = value;
			}
		}

		protected override void OnCantMove<T>(T component)
		{

		}

		protected override void OnFinishMove(){

		}
	}
}


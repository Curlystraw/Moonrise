using System;
using ItemSpace;

namespace Completed
{
	public class Character : MovingObject
	{
		//leveled up with magic character points
		private int baseHP;
		private double baseDodge;
		private double baseBlock;
		private int baseAttack;
		private double baseAccuracy;
		private int baseRange;

		//affected by items
		private int totalHP;
		private double totalDodge;
		private double totalBlock;
		private int totalAttack;
		private double totalAccuracy;
		private int totalRange;

		private int currentHP;

		private double baseSpeed;
		private double totalSpeed;


		private EquippedItemSet equippedItems;
		private Inventory inventory;

		Random rand = new Random();

		public Character () : this(100, .1, .1, 5, .9, 5, 1.0)
		{
		}

		public Character (int hp, double dodge, double block, int attack, double accuracy,int range, double speed)
		{
			baseHP = hp;
			baseBlock = block;
			baseAttack = attack;

			currentHP = baseHP;

			totalHP = baseHP;
			totalBlock = baseBlock;
			totalAttack = baseAttack;

			equippedItems = new EquippedItemSet ();
			inventory = new Inventory ();
		}
		/// <summary>
		/// Ranged attack function
		/// </summary>
		/// <returns>The attack.</returns>
		/// <param name="target">Target.</param>
		public int RangedAttack(Character target) {
			if (rand.NextDouble () <= this.TotalAccuracy) {
				if (rand.NextDouble () > target.TotalDodge) {
					int damage = this.TotalAttack + (int)(this.TotalAttack * (rand.NextDouble () / 10 - .05));
					target.CurrentHP -= damage;
					return damage;
				}
			}
			return 0;
		}
		/// <summary>
		/// Melee attack function
		/// </summary>
		/// <returns>The attack.</returns>
		/// <param name="target">Target.</param>
		public int MeleeAttack(Character target) {
			if (rand.NextDouble () <= this.TotalAccuracy) {
				if (rand.NextDouble () > target.TotalBlock) {
					int damage = this.TotalAttack + (int)(this.TotalAttack * (rand.NextDouble () / 10 - .05));
					target.CurrentHP -= damage;
					return damage;
				}
			}
			return 0;
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
		/// Remove the item from the inventory.
		/// </summary>
		/// <returns><c>true</c>, if item was removed, <c>false</c> otherwise.</returns>
		/// <param name="item">Item.</param>
		public bool RemoveItem(Item item)
		{
			return inventory.RemoveItem (item);
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
			// TODO: update stats based on changed items
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

		#region properties	
		public int TotalHP {
			get {
				return this.totalHP;
			}
			set {
				totalHP = value;
			}
		}

		public double TotalDodge {
			get {
				return this.totalDodge;
			}
			set {
				totalDodge = value;
			}
		}

		public double TotalBlock {
			get {
				return this.totalBlock;
			}
			set {
				totalBlock = value;
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

		public double TotalAccuracy {
			get {
				return this.totalAccuracy;
			}
			set {
				totalAccuracy = value;
			}
		}

		public int TotalRange {
			get {
				return this.totalRange;
			}
			set {
				totalRange = value;
			}
		}

		public double TotalSpeed {
			get {
				return this.totalSpeed;
			}
			set {
				totalSpeed = value;
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
				return this.equippedItems;
			}
			set {
				equippedItems = value;
			}
		}

		public Inventory Inventory {
			get {
				return this.inventory;
			}
			set {
				inventory = value;
			}
		}
		#endregion

		protected override void OnCantMove<T>(T component)
		{

		}

		protected override void OnFinishMove(){

		}
	}
}


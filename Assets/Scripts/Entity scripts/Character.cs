using System;
using ItemSpace;
using UnityEditor;
using UnityEngine;

namespace Completed
{
	public class Character : MovingObject
	{
		//leveled up with magic character points
		protected int baseHP;
		protected double baseDodge;
		protected double baseBlock;
		protected int baseAttack;
		protected double baseAccuracy;
		protected int baseRange;

		//affected by items
		protected int totalHP;
		protected double totalDodge;
		protected double totalBlock;
		protected int totalAttack;
		protected double totalAccuracy;
		protected int totalRange;

		protected int currentHP;

		protected double baseSpeed;
		protected double totalSpeed;

		private EquippedItemSet equippedItems;
		private Inventory inventory;

		public Character () : this(100, .1, .1, 5, .9, 5, 1.0)
		{
			currentHP = 6;
		}

		public Character (int hp, int armor, int attack, double accuracy, int range, double speed)
		{
			baseHP = hp;
			baseAttack = attack;
			baseAccuracy = accuracy;
			baseRange = range;
			baseSpeed = speed;

			currentHP = baseHP;

			totalHP = baseHP;
			totalAttack = baseAttack;
			totalAccuracy = baseAccuracy;
			totalRange = baseRange;
			totalSpeed = baseSpeed;

			equippedItems = new EquippedItemSet ();
			inventory = new Inventory ();
		}

		/// <summary>
		/// Ranged attack function
		/// </summary>
		/// <returns>The attack.</returns>
		/// <param name="target">Target.</param>
		public int RangedAttack(Character target) {
			if (UnityEngine.Random.Range (0.0f, 1.0f) <= this.TotalAccuracy) {
				if (UnityEngine.Random.Range (0.0f, 1.0f) > target.TotalDodge) {
					int damage = this.TotalAttack + (int)(this.TotalAttack * (UnityEngine.Random.Range (0.0f, 1.0f) / 10 - .05));
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
			if (UnityEngine.Random.Range (0.0f, 1.0f) <= this.TotalAccuracy) {
				if (UnityEngine.Random.Range (0.0f, 1.0f) > target.TotalBlock) {
					int damage = this.TotalAttack + (int)(this.TotalAttack * (UnityEngine.Random.Range (0.0f, 1.0f) / 10 - .05));
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
		/// Drops hp by loss
		/// </summary>
		/// <param name="loss">Loss.</param>
		public virtual void LoseHp(int loss)
		{
			currentHP -= loss;
			if (currentHP <= 0) {
				KillObject ();
			}
			return;
		}

		protected virtual void KillObject()
		{
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


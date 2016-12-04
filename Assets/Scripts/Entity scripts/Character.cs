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

		public int currentHP;

		protected double baseSpeed;
		protected double totalSpeed;

		protected EquippedItemSet equippedItems;
		protected Inventory inventory;

		public Character () : this(90, .1, .1, 5, .9, 1, 1.0)
		{
		}
			
		public Character (int hp, double dodge, double block, int attack, double accuracy,int range, double speed)
		{
			this.baseHP = hp;
			this.baseBlock = block;
			this.baseAttack = attack;
			this.baseAccuracy = accuracy;
			this.baseRange = range;
			this.baseSpeed = speed;

			this.currentHP = baseHP;
			this.totalHP = baseHP;

			this.totalBlock = baseBlock;
			this.totalAttack = baseAttack;
			this.totalAccuracy = baseAccuracy;
			this.totalRange = baseRange;
			this.totalSpeed = baseSpeed;

			this.equippedItems = new EquippedItemSet ();
			this.inventory = new Inventory ();
		}

		/// <summary>
		/// Ranged attack function
		/// </summary>
		/// <returns>The attack.</returns>
		/// <param name="target">Target.</param>
		public int RangedAttack(Character target) {
			float distance = Mathf.Sqrt (Mathf.Pow (target.transform.position.x - this.transform.position.x, 2) + Mathf.Pow (target.transform.position.y - this.transform.position.y, 2));

			if (UnityEngine.Random.Range (0.0f, 1.0f) <= this.TotalAccuracy) {
				// If distance is 1, use block instead of dodge
				double missValue = distance <= 1 ? target.TotalBlock : target.TotalDodge;

				if (UnityEngine.Random.Range (0.0f, 1.0f) > missValue) {
					int damage = this.TotalAttack + (int)(this.TotalAttack * (UnityEngine.Random.Range (0.0f, 1.0f) / 10 - .05));
					target.LoseHp(damage);
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
					target.LoseHp (damage);
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
					Item unequipped = equippedItems.Unequip (equippable.ItemClass);
					if (equippable != null)
						inventory.AddItem (unequipped);
					equippedItems.Equip (equippable);
				}
			}
			// TODO: update stats based on changed items
		}

		/// <summary>
		/// Drops hp by loss
		/// </summary>
		/// <param name="loss">Loss.</param>
		public virtual void LoseHp(int loss)
		{
			CurrentHP -= loss;
			if (CurrentHP <= 0) {
				//KillObject ();
			}
			return;
		}
			

		protected virtual void KillObject()
		{
			Destroy (this.gameObject);
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

		public int BaseAttack {
			get {
				return this.baseAttack;
			}
			set {
				baseAttack = value;
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
				if (currentHP == 0) {
					this.KillObject ();
				}

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

		protected override void OnCantMove(Transform transform)
		{

		}

		protected override void OnFinishMove(){

		}
	}
}


using System;

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



		public Character ()
		{
			baseHP = 7;
			baseArmor = 2;
			baseAttack = 3;

			totalHP = 7;
			totalArmor = 2;
			totalAttack = 3;

			currentHP = 6;
		}

		public Character (int hp, int armor, int attack)
		{
			baseHP = hp;
			baseArmor = armor;
			baseAttack = attack;

			currentHP = baseHP;
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

		protected override void OnCantMove<T>(T component)
		{

		}

		protected override void OnFinishMove(){

		}

		protected override void KillObject() {
		}

	}
}


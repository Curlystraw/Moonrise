using System;

namespace ItemSpace
{
	public abstract class EquipItem : Item
	{
		protected int attack, defense, health;

		public int Attack {
			get {
				return attack;
			}
		}

		public int Defense {
			get {
				return defense;
			}
		}

		public int Health {
			get {
				return health;
			}
		}

		public abstract override ItemClass GetItemClass();
	}
}


using System;

namespace ItemSpace
{
	public abstract class Weapon : EquipItem
	{
		protected int attack, range;

		public int Attack {	// will eventually be affected by prefix/infix/suffix
			get {
				return attack;
			}
		}

		public int Range {
			get {
				return range;
			}
		}

		public override ItemClass GetItemClass()
		{
			return ItemClass.Weapon;
		}
	}
}


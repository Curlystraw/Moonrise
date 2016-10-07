using System;

namespace ItemSpace
{
	public abstract class Weapon : EquipItem
	{
		protected int power, range;

		public int Power {
			get {
				return power;
			}
		}

		public int Range {
			get {
				return range;
			}
		}
	}
}


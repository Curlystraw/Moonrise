using System;

namespace ItemSpace
{
	public abstract class Weapon : Item
	{
		protected int power, range;

		public abstract int GetPower();
		public abstract int GetRange();

		public abstract String GetDescription();
	}
}


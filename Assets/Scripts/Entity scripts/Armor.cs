using System;

namespace ItemSpace
{
	public abstract class Armor : Item
	{
		protected int defense;

		public abstract int GetDefense();

		public abstract String GetDescription();
	}
}


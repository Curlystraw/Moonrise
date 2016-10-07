using System;

namespace ItemSpace
{
	public abstract class Helmet : Armor
	{
		protected int vision;

		public int GetVision()
		{
			return vision;
		}

		public override int GetDefense()
		{
			return defense;
		}

		public override abstract String GetDescription();
	}
}


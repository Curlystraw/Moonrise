using System;

namespace ItemSpace
{
	public class Crossbow : Weapon
	{
		public Crossbow ()
		{
			this.power = 5;
			this.range = 5;
		}

		public override int GetPower()
		{
			return power;
		}

		public override int GetRange()
		{
			return range;
		}

		public override String GetDescription()
		{
			return "This is a crossbow.";
		}
	}
}


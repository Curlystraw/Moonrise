using System;

namespace ItemSpace
{
	public class Crossbow : Weapon
	{
		public Crossbow ()
		{
			this.attack = 5;
			this.range = 5;
			this.description = "This is a crossbow.";
		}
	}
}


using System;

namespace ItemSpace
{
	public class LeatherHelmet : Helmet
	{
		public LeatherHelmet ()
		{
			this.defense = 5;
			this.vision = 10;
		}

		public override String GetDescription()
		{
			return "This is a leather helmet.";
		}
	}
}


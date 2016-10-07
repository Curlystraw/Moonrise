using System;

namespace ItemSpace
{
	public class KnightHelmet : Helmet
	{
		public KnightHelmet ()
		{
			this.defense = 10;
			this.vision = 5;
		}

		public override String GetDescription()
		{
			return "This is a knight's helmet. It provides good defense but constrains vision.";
		}
	}
}


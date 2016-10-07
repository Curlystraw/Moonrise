using System;

namespace ItemSpace
{
	public abstract class Helmet : Armor
	{
		protected int vision;

		public int Vision {
			get {
				return vision;
			}
		}
	}
}


using System;

namespace ItemSpace
{
	public abstract class Potion : ConsumeItem
	{
		public override ItemClass GetItemClass ()
		{
			return ItemClass.Potion;
		}
	}
}


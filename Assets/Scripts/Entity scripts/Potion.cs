using System;

namespace ItemSpace
{
	public abstract class Potion : ConsumeItem
	{
		public Potion()
		{
			itemClass = ItemClass.Potion;
		}
	}
}


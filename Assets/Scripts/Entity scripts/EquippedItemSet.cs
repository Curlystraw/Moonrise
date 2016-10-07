using System;

namespace ItemSpace
{
	public class EquippedItemSet
	{
		private Helmet helmet;
		private Armor armor;
		private Weapon weapon;

		public EquippedItemSet()
		{
			
		}

		public Helmet EquipHelmet(Helmet newHelmet)
		{
			Helmet oldHelmet = helmet;
			helmet = newHelmet;
			return oldHelmet;
		}
	}
}


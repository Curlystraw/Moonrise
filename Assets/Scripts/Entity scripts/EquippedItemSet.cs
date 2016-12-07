using System;
using System.Collections.Generic;
using System.Linq; // for ToList()

namespace ItemSpace
{
	public class EquippedItemSet
	{
		/* An item can only be equipped if the no item of the same type is currently equipped.
		 */

		private Dictionary<ItemClass, EquipItem> items;

		public EquippedItemSet()
		{
			items = new Dictionary<ItemClass, EquipItem>();
			items.Add (ItemClass.Helmet, null);
			items.Add (ItemClass.Weapon, null);
		}

		/// <summary>
		/// Equip the specified item.
		/// Fails if there is already an equipped item of the same item class, 
		/// or if the item class is not included in this instance.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <returns>Whether the item was successfully equipped.</returns>
		public bool Equip(EquipItem item)
		{
			ItemClass ic = item.ItemClass;
			if (items.ContainsKey (ic) && items [ic] == null) {
				items [ic] = item;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Unequip the item of the specified item class.
		/// </summary>
		/// <param name="ic">Item class.</param>
		/// <returns>The unequipped item.</returns>
		public Item Unequip(ItemClass ic)
		{
			if (items.ContainsKey (ic)) {
				Item item = items [ic];
				items [ic] = null;
				return item;
			}
			return null;
		}

		public Item Get(ItemClass ic)
		{
			if (items.ContainsKey (ic))
				return items [ic];
			return null;
		}

		public List<EquipItem> Items {
			get {
				return items.Values.ToList();
			}
		}
	}
}


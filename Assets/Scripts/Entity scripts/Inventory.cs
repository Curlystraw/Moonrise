using System;
using System.Collections.Generic;

namespace ItemSpace
{
	public class Inventory
	{
		private List<Item> items;

		public Inventory()
		{
			items = new List<Item> ();

		}

		/// <summary>
		/// Add the item.
		/// Do nothing if the item is null.
		/// </summary>
		/// <param name="item">Item.</param>
		public void AddItem(Item item)
		{
			if(item != null)
				items.Add(item);
		}

		/// <summary>
		/// Remove the item.
		/// </summary>
		/// <returns><c>true</c>, if item was removed, <c>false</c> otherwise.</returns>
		/// <param name="item">Item.</param>
		public bool RemoveItem(Item item)
		{
			return items.Remove(item);
		}

		public List<Item> Items {
			get {
				return items;
			}
		}
	}
}


using System;
using System.Collections.Generic;

namespace ItemSpace
{
	public class Inventory
	{
		private List<Item> items;

		public Inventory()
		{
			items = new List<Item>();
		}

		public void AddItem(Item item)
		{
			items.Add(item);
		}

		public bool RemoveItem(Item item)
		{
			return items.Remove(item);
		}
	}
}


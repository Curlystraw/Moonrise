using System;

namespace ItemSpace
{
	public abstract class Item
	{
		protected string name, description, type;

		public string Name {
			get {
				return name;
			}
		}

		public string Description {
			get {
				return description;
			}
		}

		public abstract ItemClass GetItemClass();

		// void PickUp();
	}
}


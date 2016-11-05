using System;

namespace ItemSpace
{
	public abstract class Item
	{
		protected string name, description;
		protected ItemClass itemClass;

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

		public ItemClass ItemClass {
			get {
				return itemClass;
			}
		}

		// void PickUp();
	}
}


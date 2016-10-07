using System;

namespace ItemSpace
{
	public abstract class Item
	{
		protected String name, description;

		public String Name {
			get {
				return name;
			}
		}

		public String Description {
			get {
				return description;
			}
		}

		// void PickUp();
	}
}


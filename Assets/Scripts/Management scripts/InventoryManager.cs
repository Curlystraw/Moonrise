using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Completed;
using ItemSpace;

public class InventoryManager : MonoBehaviour
{
	// prefab
	public GridLayoutGroup equippedGrid, inventoryGrid;
	public ItemObject itemObjPrefab;

	public void InitializeGrids(Player player)
	{
		DumpItemsIntoGrid (player.EquippedItems, equippedGrid);
		DumpItemsIntoGrid (player.Inventory, inventoryGrid);
	}

	private void DumpItemsIntoGrid<T>(IEnumerable<T> items, GridLayoutGroup grid) where T : Item
	{
		foreach (Item item in items) {
			ItemObject itemObj = (ItemObject) Instantiate (itemObjPrefab, grid.transform);
			itemObj.Item = item;
			// GameManager.instance.print (item.Name + " made");
		}
	}

	// Use this for initialization
	void Start ()
	{
		List<Item> tempList = new List<Item> ();
		for (int i = 0; i < 5; i++)
			tempList.Add (Weapon.RandomItem ());
		DumpItemsIntoGrid (tempList, equippedGrid);

		tempList = new List<Item> ();
		for (int i = 0; i < 12; i++)
			tempList.Add (Weapon.RandomItem ());
		DumpItemsIntoGrid (tempList, inventoryGrid);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}


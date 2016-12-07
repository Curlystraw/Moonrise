using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Completed;
using ItemSpace;

public class InventoryManager : MonoBehaviour
{
	// prefab
	public GridLayoutGroup equippedGrid, inventoryGrid;
	// public ItemObject itemObjPrefab;
	public Sprite itemSprite;

	public void InitializeGrids(Player player)
	{
		DumpItemsIntoGrid (player.EquippedItems, equippedGrid);
		DumpItemsIntoGrid (player.Inventory, inventoryGrid);
	}

	private void DumpItemsIntoGrid<T>(IEnumerable<T> items, GridLayoutGroup grid) where T : Item
	{
		foreach (Item item in items) {
			ItemObject itemObj = grid.gameObject.AddComponent<ItemObject> () as ItemObject;
			itemObj.item = item;
			itemObj.sprite = itemSprite;
		}
	}

	// Use this for initialization
	void Start ()
	{
		List<Item> tempList = new List<Item> ();
		tempList.Add (Weapon.RandomItem ());
		DumpItemsIntoGrid (tempList, equippedGrid);
		DumpItemsIntoGrid (tempList, inventoryGrid);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}


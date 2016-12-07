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

	private Player player;

	public void InitializeGrids()
	{
		DumpItemsIntoGrid (player.EquippedItems, equippedGrid);
		DumpItemsIntoGrid (player.Inventory, inventoryGrid);
	}

	private void DumpItemsIntoGrid<T>(IEnumerable<T> items, GridLayoutGroup grid) where T : Item
	{
		foreach (Transform child in grid.transform) {
			Destroy (child.gameObject);
		}
		foreach (Item item in items) {
			ItemObject itemObj = (ItemObject) Instantiate (itemObjPrefab, grid.transform);
			itemObj.Item = item;
			// GameManager.instance.print (item.Name + " made");
		}
	}

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform.GetComponent<Player> ();
		InitializeGrids ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// does this make sense?  can it be simplified?
		if (player.inventoryUpdated) {
			InitializeGrids ();
			player.inventoryUpdated = false;
		}
	}
}


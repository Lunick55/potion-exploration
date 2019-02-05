using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
	public Item item;

	[Range(1,999)]
	public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
	public List<ItemAmount> materials;
	[Space]
	public List<ItemAmount> results;

	public bool CanCraft(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in materials)
		{
			if(itemContainer.ItemCount(itemAmount.item) < itemAmount.Amount)
			{
				return false;
			}
		}
		return true;
	}

	public void Craft(IItemContainer itemContainer)
	{
		if (CanCraft(itemContainer))
		{
			foreach (ItemAmount itemAmount in materials)
			{
				for (int i = 0; i < itemAmount.Amount; i++)
				{
					itemContainer.RemoveItem(itemAmount.item);
				}
			}

			foreach (ItemAmount itemAmount in results)
			{
				for (int i = 0; i < itemAmount.Amount; i++)
				{
					itemContainer.AddItem(itemAmount.item);
				}
			}
		}
	}
}

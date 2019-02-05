using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PLAYER : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] Inventory inventory;
	[SerializeField] CraftingRecipe recipe;
 
	// Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			inventory.AddItem(item);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			recipe.Craft(inventory);
		}
	}
}

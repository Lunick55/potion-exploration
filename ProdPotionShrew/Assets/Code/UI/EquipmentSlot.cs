using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
	public EquipmentType equipType;

	public override bool CanRecieveItem(Item item)
	{
		if (item == null)
		{
			return true;
		}

		EquipableItem equipableItem = item as EquipableItem;
		return equipableItem != null && equipableItem.equipType == equipType;
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = equipType.ToString() + " Slot";
	}
}

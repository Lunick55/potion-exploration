using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
	public EquipmentType equipType;

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = equipType.ToString() + " Slot";
	}
}

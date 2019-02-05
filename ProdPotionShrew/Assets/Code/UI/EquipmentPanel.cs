using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotParent;
	[SerializeField] EquipmentSlot[] equipmentSlots;

	public event Action<Item> OnItemRightClickedEvent;

	private void Awake()
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
		}
	}

	private void OnValidate()
	{
		equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();

		RefreshUI();
	}

	private void RefreshUI()
	{
		int i = 0;

		for (; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].item == null)
			equipmentSlots[i].item = null;
		}
	}


	public bool AddItem(EquipableItem item, out EquipableItem previousItem)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].equipType == item.equipType)
			{
				previousItem = (EquipableItem)equipmentSlots[i].item;
				equipmentSlots[i].item = item;
				return true;
			}
		}
		previousItem = null;
		return false;
	}

	public bool RemoveItem(EquipableItem item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].item == item)
			{
				equipmentSlots[i].item = null;
				return true;
			}
		}
		return false;
	}
}

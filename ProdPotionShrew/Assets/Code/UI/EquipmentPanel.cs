using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotParent;
	[SerializeField] EquipmentSlot[] equipmentSlots;

	public event Action<ItemSlot> OnRightClickEvent;
	public event Action<ItemSlot> OnBeginDragEvent;
	public event Action<ItemSlot> OnEndDragEvent;
	public event Action<ItemSlot> OnDragEvent;
	public event Action<ItemSlot> OnDropEvent;

	private void Start()
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
			equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
			equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
			equipmentSlots[i].OnDragEvent += OnDragEvent;
			equipmentSlots[i].OnDropEvent += OnDropEvent;
		}
	}

	private void OnValidate()
	{
		equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();

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

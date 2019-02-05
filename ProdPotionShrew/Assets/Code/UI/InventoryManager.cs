using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;

	[SerializeField] Image draggableItem;

	private ItemSlot draggedSlot;

	private void Awake()
	{

		//setup events
		//Right Click
		inventory.OnRightClickEvent += Equip;
		equipmentPanel.OnRightClickEvent += Unequip;
		//Begin Drag
		inventory.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		//End Drag
		inventory.OnEndDragEvent += EndDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		//Drag
		inventory.OnDragEvent += Drag;
		equipmentPanel.OnDragEvent += Drag;
		//Drop
		inventory.OnDropEvent += Drop;
		equipmentPanel.OnDropEvent += Drop;
	}

	private void Equip(ItemSlot itemSlot)
	{
		EquipableItem equipableItem = itemSlot.item as EquipableItem;
		if (equipableItem != null)
		{
			Equip(equipableItem);
		}
	}
	private void Unequip(ItemSlot itemSlot)
	{
		EquipableItem equipableItem = itemSlot.item as EquipableItem;
		if (equipableItem != null)
		{
			Unequip(equipableItem);
		}
	}

	private void BeginDrag(ItemSlot itemSlot)
	{
		if (itemSlot.item != null)
		{
			draggedSlot = itemSlot;
			draggableItem.sprite = itemSlot.item.icon;
			//TODO: make this transform call a private variable
			draggableItem.transform.position = Input.mousePosition;
			draggableItem.enabled = true;
		}
	}

	private void EndDrag(ItemSlot itemSlot)
	{
		draggedSlot = null;
		draggableItem.enabled = false;
	}

	private void Drag(ItemSlot itemSlot)
	{
		if (draggableItem.enabled)
		{
			draggableItem.transform.position = Input.mousePosition;
		}
	}

	private void Drop(ItemSlot dropItemSlot)
	{
		if (dropItemSlot.CanRecieveItem(draggedSlot.item) && draggedSlot.CanRecieveItem(dropItemSlot.item))
		{

			EquipableItem dragItem = draggedSlot.item as EquipableItem;
			EquipableItem dropItem = dropItemSlot.item as EquipableItem;

			//used for stats, NOT IMPLEMENTED
			//if (draggedSlot is EquipmentSlot)
			//{
			//	//if (dragItem != null) dragItem.Unequip(this);
			//	//if (dropItem != null) dropItem.Equip(this);
			//}
			//if (dropItemSlot is EquipmentSlot)
			//{
			//	//if (dragItem != null) dragItem.Equip(this);
			//	//if (dropItem != null) dropItem.Unequip(this);
			//}

			Item draggedItem = draggedSlot.item;
			draggedSlot.item = dropItemSlot.item;
			dropItemSlot.item = draggedItem;
		}
	}

	public void Equip(EquipableItem item)
	{
		if (inventory.RemoveItem(item))
		{
			EquipableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if (previousItem != null)
				{
					inventory.AddItem(previousItem);
				}
			}
			else
			{
				inventory.AddItem(item);
			}
		}
	}

	public void Unequip(EquipableItem item)
	{
		if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
		{
			inventory.AddItem(item);
		}
	}
}

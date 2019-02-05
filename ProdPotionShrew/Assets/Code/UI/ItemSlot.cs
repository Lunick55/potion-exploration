using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] Image image;

	public event Action<Item> OnRightClickEvent;

	private Item _item;
	public Item item
	{
		get { return _item; }
		set
		{
			_item = value;

			if (_item == null)
			{
				image.enabled = false;
			}
			else
			{
				image.sprite = _item.icon;
				image.enabled = true;
			}
		}
	}

	protected virtual void OnValidate()
	{
		if (image == null)
			image = GetComponent<Image>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
		{
			if (item != null && OnRightClickEvent != null)
			{
				OnRightClickEvent(item);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
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

	[SerializeField] Image image;

	private void OnValidate()
	{
		if (image == null)
			image = GetComponent<Image>();
	}
}

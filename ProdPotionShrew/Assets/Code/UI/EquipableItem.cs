using UnityEngine;

public enum EquipmentType
{
	Potion1,
	Potion2,
	Potion3
}

[CreateAssetMenu]
public class EquipableItem : Item
{
	public EquipmentType equipType;
	public string element;
}

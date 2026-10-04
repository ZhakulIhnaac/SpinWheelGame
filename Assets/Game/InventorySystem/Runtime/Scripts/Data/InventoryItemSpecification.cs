using System;
using UnityEngine;

namespace Game.InventorySystem.Runtime.Scripts.Data
{
	[Serializable]
	public struct InventoryItemSpecification
	{
		[field: SerializeField] public ItemId Id { get; private set; }
		[field: SerializeField] public string DisplayName { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
	}
}

using System;
using UnityEngine;

namespace Game.InventorySystem.Runtime.Scripts.Data
{
	[Serializable]
	public struct InventoryItemEntryData
	{
		[SerializeField] private ItemId _itemId;
		[SerializeField] private int _amount;

		public ItemId ItemId => _itemId;
		public int Amount => _amount;

		public InventoryItemEntryData(ItemId itemId, int amount)
		{
			_itemId = itemId;
			_amount = amount;
		}
	}
}

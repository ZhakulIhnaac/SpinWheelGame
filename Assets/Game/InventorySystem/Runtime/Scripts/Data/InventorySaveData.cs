using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.InventorySystem.Runtime.Scripts.Data
{
	[Serializable]
	public struct InventorySaveData
	{
		[SerializeField] private List<InventoryItemEntryData> _items;

		public IReadOnlyList<InventoryItemEntryData> Items => _items;

		public InventorySaveData(List<InventoryItemEntryData> items)
		{
			_items = items;
		}
	}
}

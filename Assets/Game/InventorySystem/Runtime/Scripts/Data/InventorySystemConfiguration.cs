using System;
using UnityEngine;

namespace Game.InventorySystem.Runtime.Scripts.Data
{
	[CreateAssetMenu(fileName = "InventorySystemConfiguration", menuName = "GameConfiguration/InventorySystemConfiguration")]
	public class InventorySystemConfiguration : ScriptableObject
	{
		[SerializeField] private InventoryItemSpecification[] _itemSpecifications;

		public InventoryItemSpecification GetItemSpecification(ItemId itemId)
		{
			for (int i = 0; i < _itemSpecifications.Length; i++)
			{
				if (_itemSpecifications[i].Id == itemId)
				{
					return _itemSpecifications[i];
				}
			}

			throw new ArgumentException($"No item specification found for {itemId}", nameof(itemId));
		}
	}
}

using System;
using System.Collections.Generic;
using Game.InventorySystem.Runtime.Scripts.Data;
using UnityEngine;
using Utilities;

namespace Game.InventorySystem.Runtime.Scripts
{
	public class InventorySystemManager : SingletonMonoBehaviour<InventorySystemManager>
	{
		[SerializeField] private InventorySystemConfiguration _inventorySystemConfiguration;

		private const string _saveDataKey = "InventorySystem.SaveData";
		private const int _initialCoinAmount = 1000;

		public event Action<ItemId> ItemAmountChanged;

		private readonly Dictionary<ItemId, int> _itemAmounts = new();

		public void Initialize()
		{
			LoadSaveData();
		}

		public void AddItems(IReadOnlyDictionary<ItemId, int> items)
		{
			foreach (var (itemId, amount) in items)
			{
				SetItemAmount(itemId, GetItemAmount(itemId) + amount);
			}

			Save();
		}

		public bool TrySpendItem(ItemId itemId, int amount)
		{
			if (GetItemAmount(itemId) < amount)
			{
				return false;
			}

			SetItemAmount(itemId, GetItemAmount(itemId) - amount);
			Save();
			return true;
		}

		public int GetItemAmount(ItemId itemId) => _itemAmounts.GetValueOrDefault(itemId, 0);

		public InventoryItemSpecification GetItemSpecification(ItemId itemId) => _inventorySystemConfiguration.GetItemSpecification(itemId);

		private void SetItemAmount(ItemId itemId, int amount)
		{
			_itemAmounts[itemId] = amount;
			ItemAmountChanged?.Invoke(itemId);
		}

		private void LoadSaveData()
		{
			if (!PlayerPrefs.HasKey(_saveDataKey))
			{
				AddItems(new Dictionary<ItemId, int> { { ItemId.Coin, _initialCoinAmount } });
				return;
			}

			var saveData = JsonUtility.FromJson<InventorySaveData>(PlayerPrefs.GetString(_saveDataKey));

			foreach (var entry in saveData.Items)
			{
				SetItemAmount(entry.ItemId, entry.Amount);
			}
		}

		private void Save()
		{
			var items = new List<InventoryItemEntryData>(_itemAmounts.Count);

			foreach (var (itemId, amount) in _itemAmounts)
			{
				items.Add(new InventoryItemEntryData(itemId, amount));
			}

			PlayerPrefs.SetString(_saveDataKey, JsonUtility.ToJson(new InventorySaveData(items)));
			PlayerPrefs.Save();
		}
	}
}

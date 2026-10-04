using System;
using System.Collections.Generic;
using Game.InventorySystem.Runtime.Scripts;
using Game.InventorySystem.Runtime.Scripts.Data;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Game.SpinWheelSystem.Runtime.Scripts
{
	public class SpinWheelSystemManager : SingletonMonoBehaviour<SpinWheelSystemManager>
	{
		[SerializeField] private SpinWheelSystemClientConfiguration _spinWheelSystemClientConfiguration;
		[SerializeField] private SpinWheelSystemLogicConfiguration _spinWheelSystemLogicConfiguration;
		[SerializeField] private SpinWheelPanel _spinWheelPanel;

		public float ZoneStepWidth => _spinWheelSystemClientConfiguration.ZoneStepWidth;
		public static float ZoneStepTime => SpinWheelSystemClientConfiguration.ZoneStepTime;
		public static float WheelSpinTime => SpinWheelSystemClientConfiguration.WheelSpinTime;
		public static float RewardGiveAnimationTime => SpinWheelSystemClientConfiguration.RewardGiveAnimationTime;
		public int RevivePrice => _spinWheelSystemLogicConfiguration.RevivePrice;

		private readonly SpinWheelSliceData[] _displayingSlices = new SpinWheelSliceData[SpinWheelSystemLogicConfiguration.SpinWheelSliceCount];
		private readonly Dictionary<ItemId, int> _earnedItems = new(16);

		public SpinWheelSliceData LastSpinResult { get; private set; }
		public SpinWheelSliceData[] DisplayingSlices => _displayingSlices;
		public AudioClip ItemAddedSoundEffect => _spinWheelSystemClientConfiguration.ItemAddedSoundEffect;
		public AudioClip WheelPinTickSound => _spinWheelSystemClientConfiguration.WheelPinTickSound;
		private int _currentZoneNumber;

		public void Initialize()
		{
			_spinWheelPanel.Initialize();
			ResetSpinData();
		}

		public void OpenSpinWheelPanel()
		{
			ResetSpinData();
			SetNewItemsForCurrentZone();
			_spinWheelPanel.OpenPanel();
		}

		public bool TryToAdvanceToTheNextZone()
		{
			if (_currentZoneNumber < ZoneCount)
			{
				_currentZoneNumber++;
				return true;
			}
			
			return false;
		}
		
		public void SetNewItemsForCurrentZone()
		{
			var spinZoneId = GetSpinZoneForNumber(_currentZoneNumber);

			var arrayIterationIndex = 0;
			Array.Clear(_displayingSlices, 0, _displayingSlices.Length);

			if (spinZoneId == SpinZoneId.Basic)
			{
				_displayingSlices[arrayIterationIndex++] = SpinWheelSliceData.CreateBomb();
			}

			var zoneRewards = spinZoneId switch
							  {
								  SpinZoneId.Basic => _spinWheelSystemLogicConfiguration.BasicZoneRewards,
								  SpinZoneId.Safe  => _spinWheelSystemLogicConfiguration.SafeZoneRewards,
								  SpinZoneId.Super => _spinWheelSystemLogicConfiguration.SuperZoneRewards,
								  _                => throw new ArgumentOutOfRangeException(nameof(spinZoneId), spinZoneId, null)
							  };

			var itemAmountList = spinZoneId switch
								{
									SpinZoneId.Basic => _spinWheelSystemLogicConfiguration.BasicItemAmounts,
									SpinZoneId.Safe  => _spinWheelSystemLogicConfiguration.BasicItemAmounts,
									SpinZoneId.Super => _spinWheelSystemLogicConfiguration.SuperItemAmounts,
									_                => throw new ArgumentOutOfRangeException(nameof(spinZoneId), spinZoneId, null)
								};

			var tierWeights = zoneRewards.GetTierWeightsForZone(_currentZoneNumber);
			var amountMultiplier = GetAmountMultiplierForZone(_currentZoneNumber);

			for (int i = arrayIterationIndex; i < SpinWheelSystemLogicConfiguration.SpinWheelSliceCount; i++)
			{
				var tier = tierWeights.GetWeightedRandomIndex();
				var itemId = zoneRewards.TierItemPools[tier].TakeRandomItem();
				var amount = Mathf.Max(1, Mathf.RoundToInt(itemAmountList.TakeRandomItem() * amountMultiplier));
				_displayingSlices[i] = SpinWheelSliceData.CreateItem(itemId, amount);
			}

			_displayingSlices.Shuffle();
		}

		// Ibrahim: Any possible pity, safety or similar systems must be handled here
		public int GetRewardNumberForSpinningTheWheel()
		{
			var index = Random.Range(0, SpinWheelSystemLogicConfiguration.SpinWheelSliceCount);
			LastSpinResult = _displayingSlices[index];

			if (LastSpinResult.SliceType == SpinWheelSliceType.Item)
			{
				AddToEarnedItems(LastSpinResult);
			}

			return index + 1;
		}

		public void DoOnSpinWheelPanelClosing(bool isClosingWithRewardsEarned)
		{
			if (isClosingWithRewardsEarned)
			{
				AddEarnedItemsIntoInventory();
			}
			else
			{
				ResetSpinData();
			}
		}

		private void AddEarnedItemsIntoInventory()
		{
			InventorySystemManager.Instance.AddItems(_earnedItems);
			_earnedItems.Clear();
		}

		private void ResetSpinData()
		{
			_currentZoneNumber = 1;
			_earnedItems.Clear();
		}

		#region Utils
		private void AddToEarnedItems(SpinWheelSliceData slice)
		{
			_earnedItems.TryAdd(slice.ItemId, 0);
			_earnedItems[slice.ItemId] += slice.Amount;
		}
		#endregion

		#region Queries
		public SpinWheelPanelZoneIndicator GetZoneIndicatorPrefab() => _spinWheelSystemClientConfiguration.ZoneIndicatorPrefab;
		public SpinWheelPanelRewardIndicator GetRewardIndicatorPrefab() => _spinWheelSystemClientConfiguration.RewardIndicatorPrefab;
		public int ZoneCount => _spinWheelSystemLogicConfiguration.SpinWheelZonesCount;
		public SpinZoneId CurrentZoneId => GetSpinZoneForNumber(_currentZoneNumber);
		public Sprite GetItemIcon(ItemId itemId) => InventorySystemManager.Instance.GetItemSpecification(itemId).Icon;
		public Sprite GetSliceIcon(SpinWheelSliceData slice) => slice.SliceType == SpinWheelSliceType.Bomb ? _spinWheelSystemClientConfiguration.BombIcon : GetItemIcon(slice.ItemId);
		public SpinWheelSystemZoneSpecification GetZoneSpecification(SpinZoneId spinZoneId) => _spinWheelSystemClientConfiguration.GetZoneSpecification(spinZoneId);
		public SpinZoneId GetSpinZoneForNumber(int zoneNumber)
		{
			if (zoneNumber == 0) return SpinZoneId.Basic;
			if (zoneNumber % _spinWheelSystemLogicConfiguration.SuperZoneInterval == 0) return SpinZoneId.Super;
			if (zoneNumber % _spinWheelSystemLogicConfiguration.SafeZoneInterval == 0) return SpinZoneId.Safe;
			return SpinZoneId.Basic;
		}
		public float GetAmountMultiplierForZone(int zoneNumber) => Mathf.Min(1f + _spinWheelSystemLogicConfiguration.AmountMultiplierStepPerZone * (zoneNumber - 1), _spinWheelSystemLogicConfiguration.MaxAmountMultiplier);
		public int GetAmountOfEarnedItem(ItemId itemId) => _earnedItems.GetValueOrDefault(itemId, 0);
		public Tuple<Sprite, Sprite> GetCurrentZoneWheelSprites()
		{
			var currentZoneSpecification = _spinWheelSystemClientConfiguration.GetZoneSpecification(CurrentZoneId);
			return new Tuple<Sprite, Sprite>(currentZoneSpecification.WheelSprite, currentZoneSpecification.PinSprite);
		}
		#endregion

		#region Tests
		public void TestOpenBombOverlay()
		{
			_spinWheelPanel.TestOpenBombOverlay();
		}
		#endregion
		
	}
}

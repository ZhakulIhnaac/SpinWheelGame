using System;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using Utilities;
using Utils.Singleton;
using Random = UnityEngine.Random;

namespace Game.SpinWheelSystem.Runtime.Scripts
{
	public class SpinWheelSystemManager : SingletonMonoBehaviour<SpinWheelSystemManager>
	{
		[SerializeField] private SpinWheelSystemClientConfiguration _spinWheelSystemClientConfiguration;
		[SerializeField] private SpinWheelPanel _spinWheelPanel;

		public float ZoneStepWidth => _spinWheelSystemClientConfiguration.ZoneStepWidth;
		public static float ZoneStepTime => SpinWheelSystemClientConfiguration.ZoneStepTime;
		public static float WheelSpinTime => SpinWheelSystemClientConfiguration.WheelSpinTime;
		public static float RewardGiveAnimationTime => SpinWheelSystemClientConfiguration.RewardGiveAnimationTime;

		private readonly SpinWheelItemDto[] _displayingSpinWheelItems = new SpinWheelItemDto[SpinWheelSystemLogicConfiguration.SpinWheelItemsCount];
		private readonly Dictionary<SpinWheelItemId, int> _earnedItems = new(16);

		public SpinWheelItemDto LastItemEarned { get; private set; }
		public SpinWheelItemDto[] DisplayingSpinWheelItems => _displayingSpinWheelItems;
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
			Array.Clear(_displayingSpinWheelItems, 0, _displayingSpinWheelItems.Length);

			if (spinZoneId == SpinZoneId.Basic)
			{
				_displayingSpinWheelItems[arrayIterationIndex++] = new SpinWheelItemDto(SpinWheelItemId.Bomb, 1);
			}

			var itemsList = spinZoneId switch
							{
								SpinZoneId.Basic => SpinWheelSystemLogicConfiguration.BasicItems,
								SpinZoneId.Safe  => SpinWheelSystemLogicConfiguration.BasicItems,
								SpinZoneId.Super => SpinWheelSystemLogicConfiguration.SuperItems,
								_                => SpinWheelSystemLogicConfiguration.BasicItems
							};

			var itemCountList = spinZoneId switch
								{
									SpinZoneId.Basic => SpinWheelSystemLogicConfiguration.BasicItemCounts,
									SpinZoneId.Safe  => SpinWheelSystemLogicConfiguration.BasicItemCounts,
									SpinZoneId.Super => SpinWheelSystemLogicConfiguration.SuperItemCounts,
									_                => SpinWheelSystemLogicConfiguration.BasicItemCounts
								};

			for (int i = arrayIterationIndex; i < SpinWheelSystemLogicConfiguration.SpinWheelItemsCount; i++)
			{
				_displayingSpinWheelItems[i] = new SpinWheelItemDto(itemsList.TakeRandomItem(), itemCountList.TakeRandomItem());
			}

			_displayingSpinWheelItems.Shuffle();
		}

		// Ibrahim: Any possible pity, safety or similar systems must be handled here
		public int GetRewardNumberForSpinningTheWheel()
		{
			var index = Random.Range(0, SpinWheelSystemLogicConfiguration.SpinWheelItemsCount);

			if (_displayingSpinWheelItems[index].SpinWheelItemId != SpinWheelItemId.Bomb)
			{
				AddToEarnedItems(_displayingSpinWheelItems[index]);
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
			foreach (var iteratingEarnedItem in _earnedItems)
			{
				Debug.Log($"{iteratingEarnedItem.Value} {_spinWheelSystemClientConfiguration.GetItemSpecification(iteratingEarnedItem.Key).DisplayName}(s) added into the inventory!");
			}

			_earnedItems.Clear();
		}

		private void ResetSpinData()
		{
			_currentZoneNumber = 1;
			_earnedItems.Clear();
		}

		#region Utils
		private void AddToEarnedItems(SpinWheelItemDto itemDto)
		{
			_earnedItems.TryAdd(itemDto.SpinWheelItemId, 0);
			_earnedItems[itemDto.SpinWheelItemId] += itemDto.Amount;
			LastItemEarned = itemDto;
		}
		#endregion

		#region Queries
		public SpinWheelPanelZoneIndicator GetZoneIndicatorPrefab() => _spinWheelSystemClientConfiguration.ZoneIndicatorPrefab;
		public SpinWheelPanelRewardIndicator GetRewardIndicatorPrefab() => _spinWheelSystemClientConfiguration.RewardIndicatorPrefab;
		public int ZoneCount => SpinWheelSystemLogicConfiguration.SpinWheelZonesCount;
		public SpinZoneId CurrentZoneId => GetSpinZoneForNumber(_currentZoneNumber);
		public SpinWheelSystemItemSpecification GetItemSpecification(SpinWheelItemId spinWheelItemId) => _spinWheelSystemClientConfiguration.GetItemSpecification(spinWheelItemId);
		public SpinWheelSystemZoneSpecification GetZoneSpecification(SpinZoneId spinZoneId) => _spinWheelSystemClientConfiguration.GetZoneSpecification(spinZoneId);
		public SpinZoneId GetSpinZoneForNumber(int zoneNumber)
		{
			if (zoneNumber == 0) return SpinZoneId.Basic;
			if (zoneNumber % SpinWheelSystemLogicConfiguration.SuperZoneInterval == 0) return SpinZoneId.Super;
			if (zoneNumber % SpinWheelSystemLogicConfiguration.SafeZoneInterval == 0) return SpinZoneId.Safe;
			return SpinZoneId.Basic;
		}
		public int GetAmountOfEarnedItem(SpinWheelItemId itemId) => _earnedItems.GetValueOrDefault(itemId, 0);
		public Tuple<Sprite, Sprite> GetCurrentZoneWheelSprites()
		{
			var currentZoneSpecification = _spinWheelSystemClientConfiguration.GetZoneSpecification(CurrentZoneId);
			return new Tuple<Sprite, Sprite>(currentZoneSpecification.WheelSprite, currentZoneSpecification.PinSprite);
		}
		#endregion
	}
}

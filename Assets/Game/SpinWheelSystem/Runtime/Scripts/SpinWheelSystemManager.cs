using System;
using System.Collections.Generic;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using Utils.Singleton;
using Random = UnityEngine.Random;

namespace Game.SpinWheelSystem.Runtime.Scripts
{
	public class SpinWheelSystemManager : SingletonMonoBehaviour<SpinWheelSystemManager>
	{
		[SerializeField] private SpinWheelSystemClientConfiguration _spinWheelSystemClientConfiguration;
		
		public float ZoneStepWidth => _spinWheelSystemClientConfiguration.ZoneStepWidth;
		public static float ZoneStepTime => SpinWheelSystemClientConfiguration.ZoneStepTime;
		public static float WheelSpinTime => SpinWheelSystemClientConfiguration.WheelSpinTime;
		public static float RewardGiveAnimationTime => SpinWheelSystemClientConfiguration.RewardGiveAnimationTime;

		private readonly SpinWheelItemDto[] _displayingSpinWheelItems = new SpinWheelItemDto[SpinWheelSystemLogicConfiguration.SpinWheelItemsCount];
		private readonly List<SpinWheelItemDto> _earnedItems = new(16);
		
		public SpinWheelItemDto[] GetItemsForSpinWheel(SpinZoneId spinZoneId)
		{
			var arrayIterationIndex = 0;
			Array.Clear(_displayingSpinWheelItems, 0, _displayingSpinWheelItems.Length);
			
			if (spinZoneId == SpinZoneId.Basic)
			{
				_displayingSpinWheelItems[arrayIterationIndex++] = new SpinWheelItemDto(SpinWheelItem.Bomb, 1);
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

			return _displayingSpinWheelItems;
		}

		// Ibrahim: Any possible pity, safety or similar systems must be handled here
		public int GetRewardNumberForSpinningTheWheel()
		{
			var number = Random.Range(1, SpinWheelSystemLogicConfiguration.SpinWheelZonesCount + 1);

			if (_displayingSpinWheelItems[number].SpinWheelItem != SpinWheelItem.Bomb)
			{
				_earnedItems.Add(_displayingSpinWheelItems[number]);
			}
			
			return number;
		}

		public void AddEarnedItemsIntoInventory()
		{
			foreach (var iteratingEarnedItem in _earnedItems)
			{
				Debug.Log($"{iteratingEarnedItem.Amount} {_spinWheelSystemClientConfiguration.GetItemSpecification(iteratingEarnedItem.SpinWheelItem).DisplayName}(s) added into the inventory!");	
			}

			_earnedItems.Clear();
		}

		#region Queries
		public SpinWheelPanelZoneIndicator GetZoneIndicatorPrefab() => _spinWheelSystemClientConfiguration.ZoneIndicatorPrefab;
		public int GetZoneCount() => SpinWheelSystemLogicConfiguration.SpinWheelZonesCount;
		public SpinWheelSystemItemSpecification GetItemSpecification(SpinWheelItem spinWheelItem) => _spinWheelSystemClientConfiguration.GetItemSpecification(spinWheelItem);
		public SpinWheelSystemZoneSpecification GetZoneSpecification(SpinZoneId spinZoneId) => _spinWheelSystemClientConfiguration.GetZoneSpecification(spinZoneId);
		public SpinZoneId GetSpinZoneForNumber(int zoneNumber)
		{
			if (zoneNumber == 0) return SpinZoneId.Basic;
			if (zoneNumber % SpinWheelSystemLogicConfiguration.SuperZoneInterval == 0) return SpinZoneId.Super;
			if (zoneNumber % SpinWheelSystemLogicConfiguration.SafeZoneInterval == 0) return SpinZoneId.Safe;
			return SpinZoneId.Basic;
		}
		#endregion
	}
}

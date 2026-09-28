using System;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using Utils.Singleton;

namespace Game.SpinWheelSystem.Runtime.Scripts
{
	public class SpinWheelSystemManager : SingletonMonoBehaviour<SpinWheelSystemManager>
	{
		[SerializeField] private SpinWheelSystemClientConfiguration _spinWheelSystemClientConfiguration;

		private Tuple<SpinWheelItem, int>[] _spinWheelItems = new Tuple<SpinWheelItem, int>[SpinWheelSystemLogicConfiguration.SpinWheelItemsCount];
		public float ZoneStepWidth => _spinWheelSystemClientConfiguration.ZoneStepWidth;
		public float ZoneStepTime => SpinWheelSystemClientConfiguration.ZoneStepTime;

		public Tuple<SpinWheelItem, int>[] GetItemsForSpinWheel(SpinZoneId spinZoneId)
		{
			var arrayIterationIndex = 0;

			if (spinZoneId == SpinZoneId.Basic)
			{
				_spinWheelItems[arrayIterationIndex++] = new Tuple<SpinWheelItem, int>(SpinWheelItem.Bomb, 1);
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
				_spinWheelItems[i] = new Tuple<SpinWheelItem, int>(itemsList.TakeRandomItem(), itemCountList.TakeRandomItem());
			}

			return _spinWheelItems;
		}

		#region Queries
		public SpinWheelPanelZoneIndicator GetZoneIndicatorPrefab() => _spinWheelSystemClientConfiguration.ZoneIndicatorPrefab;
		public int GetZoneCount() => SpinWheelSystemLogicConfiguration.SpinWheelZonesCount;
		public SpinWheelSystemItemSpecification GetItemSpecification(SpinWheelItem spinWheelItem) => _spinWheelSystemClientConfiguration.GetItemSpecification(spinWheelItem);
		public SpinWheelSystemZoneSpecification GetZoneSpecification(SpinZoneId spinZoneId) => _spinWheelSystemClientConfiguration.GetZoneSpecification(spinZoneId);
		#endregion
		public SpinZoneId GetSpinZoneForNumber(int zoneNumber)
		{
			if (zoneNumber == 0) return SpinZoneId.Basic;
			if (zoneNumber % SpinWheelSystemLogicConfiguration.SuperZoneInterval == 0) return SpinZoneId.Super;
			if (zoneNumber % SpinWheelSystemLogicConfiguration.SafeZoneInterval == 0) return SpinZoneId.Safe;
			return SpinZoneId.Basic;
		}
	}
}

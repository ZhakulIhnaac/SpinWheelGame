using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[CreateAssetMenu(fileName = "SpinWheelSystemClientConfiguration", menuName = "GameConfiguration/SpinWheelSystemClientConfiguration")]
	public class SpinWheelSystemClientConfiguration : ScriptableObject
	{
		[SerializeField] private SpinWheelSystemItemSpecification[] _spinWheelSystemItemSpecifications;
		[SerializeField] private SpinWheelSystemItemSpecification _fallbackItemSpecification;
		[SerializeField] private SpinWheelSystemZoneSpecification[] _spinWheelSystemZoneSpecifications;
		[field: SerializeField] public SpinWheelPanelZoneIndicator ZoneIndicatorPrefab { get; private set; }

		public const float ZoneStepTime = 1f;
		public float ZoneStepWidth => _zoneStepWidth < 0 ? _zoneStepWidth = ZoneIndicatorPrefab.Width : _zoneStepWidth;
		private float _zoneStepWidth = -1f;
		
		public SpinWheelSystemItemSpecification GetItemSpecification(SpinWheelItem spinWheelItem)
		{
			for (int i = 0; i < _spinWheelSystemItemSpecifications.Length; i++)
			{
				if (_spinWheelSystemItemSpecifications[i].Id == spinWheelItem)
				{
					return _spinWheelSystemItemSpecifications[i];
				}
			}

			return _fallbackItemSpecification;
		}
		
		public SpinWheelSystemZoneSpecification GetZoneSpecification(SpinZoneId spinZoneId)
		{
			for (int i = 0; i < _spinWheelSystemItemSpecifications.Length; i++)
			{
				if (_spinWheelSystemZoneSpecifications[i].Id == spinZoneId)
				{
					return _spinWheelSystemZoneSpecifications[i];
				}
			}

			return _spinWheelSystemZoneSpecifications[0];
		}
	}
}

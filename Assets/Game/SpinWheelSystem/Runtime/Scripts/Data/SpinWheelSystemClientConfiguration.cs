using AssetKits.ParticleImage;
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
		
		public const float ZoneStepTime = 1f;
		public const float WheelSpinTime = 3f;
		public const float RewardGiveAnimationTime = 1.5f;

		#region Prefabs
		[field: SerializeField] public SpinWheelPanelZoneIndicator ZoneIndicatorPrefab { get; private set; }
		[field: SerializeField] public SpinWheelPanelRewardIndicator RewardIndicatorPrefab { get; private set; }
		#endregion
		
		#region Sounds
		[field: SerializeField] public AudioClip ItemAddedSoundEffect { get; private set; }
		[field: SerializeField] public AudioClip WheelPinTickSound { get; private set; }
		#endregion

		public float ZoneStepWidth => _zoneStepWidth < 0 ? _zoneStepWidth = ZoneIndicatorPrefab.Width : _zoneStepWidth;
		private float _zoneStepWidth = -1f;
		
		public SpinWheelSystemItemSpecification GetItemSpecification(SpinWheelItemId spinWheelItemId)
		{
			for (int i = 0; i < _spinWheelSystemItemSpecifications.Length; i++)
			{
				if (_spinWheelSystemItemSpecifications[i].Id == spinWheelItemId)
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

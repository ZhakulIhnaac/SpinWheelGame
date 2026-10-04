using UnityEngine;
using Utilities;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[CreateAssetMenu(fileName = "SpinWheelSystemLogicConfiguration", menuName = "GameConfiguration/SpinWheelSystemLogicConfiguration")]
	public class SpinWheelSystemLogicConfiguration : ScriptableObject
	{
		public const int SpinWheelSliceCount = 8;

		[field: SerializeField] public int SpinWheelZonesCount { get; private set; }
		[field: SerializeField] public int SuperZoneInterval { get; private set; }
		[field: SerializeField] public int SafeZoneInterval { get; private set; }
		[field: SerializeField] public int RevivePrice { get; private set; }

		[field: SerializeField] public SpinWheelZoneRewardSpecification BasicZoneRewards { get; private set; }
		[field: SerializeField] public SpinWheelZoneRewardSpecification SafeZoneRewards { get; private set; }
		[field: SerializeField] public SpinWheelZoneRewardSpecification SuperZoneRewards { get; private set; }

		[field: SerializeField] public RandomWeighedItemBag<int> BasicItemAmounts { get; private set; }
		[field: SerializeField] public RandomWeighedItemBag<int> SuperItemAmounts { get; private set; }
		[field: SerializeField] public float AmountMultiplierStepPerZone { get; private set; }
		[field: SerializeField] public float MaxAmountMultiplier { get; private set; }
	}
}

using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[CreateAssetMenu(fileName = "SpinWheelSystemClientConfiguration", menuName = "GameConfiguration/SpinWheelSystemClientConfiguration")]
	public class SpinWheelSystemClientConfiguration : ScriptableObject
	{
		[SerializeField] private SpinWheelSystemItemSpecification[] _spinWheelSystemItemSpecifications;
		[SerializeField] private SpinWheelSystemItemSpecification _fallbackItemSpecification;
		
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
	}
}

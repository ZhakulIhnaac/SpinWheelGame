using System;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[Serializable]
	public struct SpinWheelSystemItemSpecification
	{
		[field: SerializeField] public SpinWheelItemId Id { get; private set; }
		[field: SerializeField] public string DisplayName { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
	}
}

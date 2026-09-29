using System;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[Serializable]
	public struct SpinWheelSystemZoneSpecification
	{
		[field: SerializeField] public SpinZoneId Id { get; private set; }
		[field: SerializeField] public Color ZoneTextColor { get; private set; }
		[field: SerializeField] public Color ZoneIndicatorBackgroundColor { get; private set; }
		[field: SerializeField] public Sprite WheelSprite { get; private set; }
		[field: SerializeField] public Sprite PinSprite { get; private set; }
	}
}

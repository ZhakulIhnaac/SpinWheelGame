using System;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[Serializable]
	public struct SpinWheelTierCheckpointSpecification
	{
		[field: SerializeField] public int ZoneNumber { get; private set; }
		[field: SerializeField] public float[] TierWeights { get; private set; }
	}
}

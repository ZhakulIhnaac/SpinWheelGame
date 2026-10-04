using System;
using System.Collections.Generic;
using Game.InventorySystem.Runtime.Scripts.Data;
using UnityEngine;
using Utilities;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	[Serializable]
	public class SpinWheelZoneRewardSpecification
	{
		[field: SerializeField] public RandomWeighedItemBag<ItemId>[] TierItemPools { get; private set; }
		[field: SerializeField] public SpinWheelTierCheckpointSpecification[] TierCheckpoints { get; private set; }

		// Checkpoints must be sorted by ascending ZoneNumber.
		public IReadOnlyList<float> GetTierWeightsForZone(int zoneNumber)
		{
			var firstCheckpoint = TierCheckpoints[0];
			var lastCheckpoint = TierCheckpoints[^1];

			if (zoneNumber <= firstCheckpoint.ZoneNumber) return firstCheckpoint.TierWeights;
			if (zoneNumber >= lastCheckpoint.ZoneNumber) return lastCheckpoint.TierWeights;

			var nextIndex = 1;
			while (TierCheckpoints[nextIndex].ZoneNumber <= zoneNumber) nextIndex++;

			var previous = TierCheckpoints[nextIndex - 1];
			var next = TierCheckpoints[nextIndex];
			var t = (float)(zoneNumber - previous.ZoneNumber) / (next.ZoneNumber - previous.ZoneNumber);
			var weights = new float[TierItemPools.Length];

			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] = Mathf.Lerp(previous.TierWeights[i], next.TierWeights[i], t);
			}

			return weights;
		}
	}
}

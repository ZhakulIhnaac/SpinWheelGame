using System;
using UnityEngine;

namespace Utilities
{
	[Serializable]
	public struct RandomWeightedItemBagEntry<T>
	{
		[field: SerializeField] public T Item { get; private set; }
		[field: SerializeField] public int Weight { get; private set; }
	}
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
	[Serializable]
	public class RandomWeighedItemBag<T>
	{
		[SerializeField] private RandomWeightedItemBagEntry<T>[] _bagEntries;

		public T TakeRandomItem()
		{
			var totalWeight = 0;

			for (int i = 0; i < _bagEntries.Length; i++)
			{
				totalWeight += _bagEntries[i].Weight;
			}

			var randomNumber = Random.Range(0, totalWeight);

			for (int i = 0; i < _bagEntries.Length; i++)
			{
				randomNumber -= _bagEntries[i].Weight;

				if (randomNumber < 0)
				{
					return _bagEntries[i].Item;
				}
			}
			
			throw new Exception("Should never happen");
		}
	}
}

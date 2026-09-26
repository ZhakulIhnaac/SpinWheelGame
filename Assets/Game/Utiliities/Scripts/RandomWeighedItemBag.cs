using UnityEngine;
namespace Utils.Singleton
{
	public class RandomWeighedItemBag<T>
	{
		private RandomWeightedItemBagEntry<T>[] _bagEntries;
		
		private int _totalWeight;
		
		public RandomWeighedItemBag(RandomWeightedItemBagEntry<T>[] bagEntries)
		{
			_bagEntries = bagEntries;
			
			foreach (var bagEntry in _bagEntries)
			{
				_totalWeight += bagEntry.Weight;
			}
		}

		public T TakeRandomItem()
		{
			var randomNumber = Random.Range(0, _totalWeight);

			for (int i = 0; i < _bagEntries.Length; i++)
			{
				randomNumber -= _bagEntries[i].Weight;

				if (randomNumber <= 0)
				{
					return _bagEntries[i].Item;
				}
			}
			
			throw new System.Exception("Should never happen");
		}
	}
}

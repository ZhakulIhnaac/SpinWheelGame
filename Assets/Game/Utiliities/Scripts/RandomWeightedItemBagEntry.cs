namespace Utilities
{
	public struct RandomWeightedItemBagEntry<T>
	{
		public T Item { get; private set; }
		public int Weight { get; private set; }
		
		public RandomWeightedItemBagEntry(T item, int weight)
		{
			Item = item;
			Weight = weight;
		}
	}
}

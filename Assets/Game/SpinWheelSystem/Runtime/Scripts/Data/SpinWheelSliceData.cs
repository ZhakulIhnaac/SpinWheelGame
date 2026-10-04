using Game.InventorySystem.Runtime.Scripts.Data;

namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	public struct SpinWheelSliceData
	{
		public SpinWheelSliceType SliceType { get; private set; }
		public ItemId ItemId { get; private set; }
		public int Amount { get; private set; }

		public static SpinWheelSliceData CreateBomb() => new() { SliceType = SpinWheelSliceType.Bomb };

		public static SpinWheelSliceData CreateItem(ItemId itemId, int amount) => new() { SliceType = SpinWheelSliceType.Item, ItemId = itemId, Amount = amount };
	}
}

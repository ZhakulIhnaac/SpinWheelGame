namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	public struct SpinWheelItemDto
	{
		public SpinWheelItem SpinWheelItem { get; private set; }
		public int Amount { get; private set; }

		public SpinWheelItemDto(SpinWheelItem spinWheelItem, int amount)
		{
			SpinWheelItem = spinWheelItem;
			Amount = amount;
		}
	}
}

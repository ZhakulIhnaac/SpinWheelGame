namespace Game.SpinWheelSystem.Runtime.Scripts.Data
{
	public struct SpinWheelItemDto
	{
		public SpinWheelItemId SpinWheelItemId { get; private set; }
		public int Amount { get; private set; }

		public SpinWheelItemDto(SpinWheelItemId spinWheelItemId, int amount)
		{
			SpinWheelItemId = spinWheelItemId;
			Amount = amount;
		}
	}
}

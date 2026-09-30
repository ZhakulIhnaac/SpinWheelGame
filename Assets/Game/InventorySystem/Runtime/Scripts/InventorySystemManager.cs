using System;
using Utilities;

namespace Game.InventorySystem.Runtime.Scripts
{
	public class InventorySystemManager : SingletonMonoBehaviour<InventorySystemManager>
	{
		public event Action CoinAmountChanged;
		
		public int CoinAmount { get; private set; }

		public void Initialize()
		{
			SetCoinAmount(1000);
		}
		
		public bool TrySpendCoin(int amount)
		{
			if (CoinAmount < amount)
			{
				return false;
			}

			CoinAmount -= amount;
			CoinAmountChanged?.Invoke();
			return true;
		}

		private void SetCoinAmount(int amount) => CoinAmount = amount;
	}
}

using DG.Tweening;
using Game.InventorySystem.Runtime.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Game.InventorySystem.Runtime.Scripts.Elements
{
	public class ResourceAmountIndicator : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _amountText;

		private Tween _amountChangeAnimation;
		private int _displayingAmount;

		public void Initialize()
		{
			InventorySystemManager.Instance.ItemAmountChanged += OnItemAmountChanged;
			SetAmount(InventorySystemManager.Instance.GetItemAmount(ItemId.Coin));
		}

		private void OnItemAmountChanged(ItemId itemId)
		{
			if (itemId != ItemId.Coin) return;

			PlayAmountChangeAnimation();
		}

		private void PlayAmountChangeAnimation()
		{
			var initialAmount = _displayingAmount;
			var targetAmount = InventorySystemManager.Instance.GetItemAmount(ItemId.Coin);

			_amountChangeAnimation?.Kill();

			_amountChangeAnimation = DOVirtual.Int(initialAmount, targetAmount, 0.5f, SetAmount)
											  .SetEase(Ease.Linear);

			_amountChangeAnimation.Play();
		}

		private void SetAmount(int newAmount)
		{
			_displayingAmount = newAmount;
			_amountText.text = $"{_displayingAmount}";
		}

		private void OnDestroy()
		{
			_amountChangeAnimation?.Kill(true);
		}
	}
}

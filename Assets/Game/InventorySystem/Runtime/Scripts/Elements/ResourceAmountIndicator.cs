using System;
using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
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
			InventorySystemManager.Instance.CoinAmountChanged += OnCoinAmountChanged;
			SetAmount(InventorySystemManager.Instance.CoinAmount);
		}
		
		private void OnCoinAmountChanged()
		{
			PlayAmountChangeAnimation();
		}
		
		private void PlayAmountChangeAnimation()
		{
			var initialAmount = _displayingAmount;
			var targetAmount = InventorySystemManager.Instance.CoinAmount;

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

using System;
using DG.Tweening;
using Game.SharedGameSystems.SoundSystem.Scripts.Controllers;
using Game.SharedGameSystems.SoundSystem.Scripts.Data;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelRewardIndicator : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _amountText;
		[SerializeField] private CanvasGroup _bodyCanvasGroup;
		[SerializeField] private RectTransform _bodyTransform;
		[field: SerializeField] public RectTransform AttractorTargetPosition;

		private Sequence _openingSequence;
		private Tween _amountUpdateTween;
		private Tween _itemAddedEffectTween;
		private SpinWheelItemId _itemId;
		private int _currentDisplayAmount;

		public void Initialize(SpinWheelItemId itemId)
		{
			_itemId = itemId;
			SetDisplayAmount(0);
			_icon.sprite = SpinWheelSystemManager.Instance.GetItemSpecification(_itemId).Icon;
			_bodyTransform.localScale = Vector3.one * 3f;
			_bodyCanvasGroup.alpha = 0;
			PlayOpeningAnimation();
		}

		private void PlayOpeningAnimation()
		{
			_openingSequence?.Kill();
			_openingSequence = DOTween.Sequence();

			_openingSequence.Append
				(
				 _bodyTransform.DOScale(Vector3.one, 0.3f)
				);

			_openingSequence.Join(
								  _bodyCanvasGroup.DOFade(1, 0.3f)
								 );

			_openingSequence.Play();
		}

		public void PlayAmountUpdateAnimation()
		{
			_amountUpdateTween?.Kill();
			_amountUpdateTween = DOVirtual.Int(_currentDisplayAmount, SpinWheelSystemManager.Instance.GetAmountOfEarnedItem(_itemId), 0.5f, SetDisplayAmount);
		}

		public void PlayItemAddedEffect()
		{
			_itemAddedEffectTween?.Kill();

			_icon.transform.localScale = Vector2.one;
			_itemAddedEffectTween = _icon.transform.DOScale(Vector2.one * 1.05f, 0.02f).SetLoops(-1, LoopType.Yoyo);

			SoundSystemManager.Instance.PlaySoundEffectOnce(SpinWheelSystemManager.Instance.ItemAddedSoundEffect, SoundEffectPitchMode.GetHigherPitch, $"itemAddedSoundEffect", 0.015f);
		}

		private void SetDisplayAmount(int value)
		{
			_currentDisplayAmount = value;
			_amountText.text = $"{_currentDisplayAmount}";
		}

		private void OnDestroy()
		{
			_openingSequence?.Kill(true);
			_amountUpdateTween?.Kill(true);
			_itemAddedEffectTween?.Kill(true);
		}
	}
}

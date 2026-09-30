using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelWheelElement : MonoBehaviour
	{
		[field: SerializeField] public RectTransform RectTransform;
		[field: SerializeField] public Transform EarnedRewardPosition;
		[SerializeField] private Image _pin;
		[SerializeField] private Image _wheel;
		[SerializeField] private SpinWheelItemIndicator[] _wheelItemIndicators;

		private const float _spinWheelItemStepAngle = 360f / SpinWheelSystemLogicConfiguration.SpinWheelItemsCount;
		
		private Sequence _spinSequence;
		private Sequence _zoneChangeSequence;
		private Vector2 _originalAnchoredPosition;

		public void Initialize()
		{
			_originalAnchoredPosition = RectTransform.anchoredPosition;
			ResetElement();
		}

		public Tween GetOpeningAnimation()
		{
			RectTransform.anchoredPosition = new Vector2(_originalAnchoredPosition.x, -Screen.height);
			return RectTransform.DOAnchorPosY(_originalAnchoredPosition.y, 0.6f)
								.SetEase(Ease.OutCubic);
		}

		public void SpinWheelToTheItem(int itemNumber)
		{
			var totalSpinAngle = 40 + 360f * 8f + (360f - (itemNumber - 1) * _spinWheelItemStepAngle);

			_spinSequence?.Kill();
			_spinSequence = DOTween.Sequence();

			_spinSequence.Append
				(
					transform.DOScale(Vector3.one * 1.2f, 0.2f)
				 );
			
			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, 40), 0.3f)
					   .SetRelative(true)
				);

			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, -totalSpinAngle), SpinWheelSystemManager.WheelSpinTime - 0.7f)
					   .SetRelative(true)
					   .SetEase(Ease.OutSine)
				);

			_spinSequence.Append
				(
				 transform.DOScale(Vector3.one, 0.2f)
				);

			_spinSequence.Play();
		}

		public void PlayUpdateWithZoneChangeAnimation()
		{
			_zoneChangeSequence?.Kill();
			_zoneChangeSequence = DOTween.Sequence();

			_zoneChangeSequence.SetDelay(0.5f);
			
			_zoneChangeSequence.Append
				(
				 RectTransform.DOAnchorPosY(-Screen.height, 0.3f)
								  .SetRelative(true)
								  .SetEase(Ease.InSine)
				);

			_zoneChangeSequence.AppendCallback(UpdateForCurrentZone);

			_zoneChangeSequence.Append
				(
				 RectTransform.DOAnchorPosY(0, 0.3f)
								  .SetEase(Ease.InSine)
				);

			_zoneChangeSequence.Play();
		}

		public void UpdateForCurrentZone()
		{
			_wheel.transform.rotation = Quaternion.identity;
			var wheelDisplayItems = SpinWheelSystemManager.Instance.DisplayingSpinWheelItems;
			
			for (int i = 0; i < _wheelItemIndicators.Length; i++)
			{
				_wheelItemIndicators[i].SetItemIdAndAmount(wheelDisplayItems[i]);
			}

			var sprites = SpinWheelSystemManager.Instance.GetCurrentZoneWheelSprites();
			_wheel.sprite = sprites.Item1;
			_pin.sprite = sprites.Item2;
		}

		public void ResetElement()
		{
			_pin.rectTransform.localRotation = Quaternion.identity;
			_wheel.rectTransform.localRotation = Quaternion.identity;
		}

		private void OnDestroy()
		{
			_spinSequence?.Kill(true);
			_zoneChangeSequence?.Kill(true);
		}
	}
}

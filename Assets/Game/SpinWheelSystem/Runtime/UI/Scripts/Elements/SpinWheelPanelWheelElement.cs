using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelWheelElement : MonoBehaviour
	{
		[field: SerializeField] public RectTransform SelfRectTransform;
		[field: SerializeField] public Transform EarnedRewardPosition;
		[SerializeField] private Image _pin;
		[SerializeField] private Image _wheel;
		[SerializeField] private SpinWheelItemIndicator[] _wheelItemIndicators;

		private const float _spinWheelItemStepAngle = 360f / SpinWheelSystemLogicConfiguration.SpinWheelItemsCount;
		
		private Sequence _spinSequence;
		private Sequence _zoneChangeSequence;

		public void Initialize()
		{
			ResetElement();
		}

		public void SpinWheelToTheItem(int itemNumber)
		{
			_spinSequence?.Kill();
			_spinSequence = DOTween.Sequence();

			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, 40), 0.3f)
					   .SetRelative(true)
				);

			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, -360f * 6f), SpinWheelSystemManager.WheelSpinTime - 0.5f)
					   .SetRelative(true)
					   .SetEase(Ease.Linear)
				);

			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, _spinWheelItemStepAngle * itemNumber - 40), 0.2f)
					   .SetRelative(true)
				);

			_spinSequence.Play();
		}

		public void PlayUpdateWithZoneChangeAnimation()
		{
			_zoneChangeSequence?.Kill();
			_zoneChangeSequence = DOTween.Sequence();

			_zoneChangeSequence.Append
				(
				 SelfRectTransform.DOAnchorPosY(-Screen.height, 0.3f)
								  .SetRelative(true)
								  .SetEase(Ease.InSine)
				);

			_zoneChangeSequence.AppendCallback(UpdateForCurrentZone);

			_zoneChangeSequence.Append
				(
				 SelfRectTransform.DOAnchorPosY(0, 0.3f)
								  .SetEase(Ease.InSine)
				);

			_zoneChangeSequence.Play();
		}

		public void UpdateForCurrentZone()
		{
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

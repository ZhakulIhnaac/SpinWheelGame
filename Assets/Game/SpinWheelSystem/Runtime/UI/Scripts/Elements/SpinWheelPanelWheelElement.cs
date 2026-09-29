using System;
using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelWheelElement : MonoBehaviour
	{
		[field: SerializeField] public Transform EarnedRewardPosition;
		[SerializeField] private Image _pin;
		[SerializeField] private Image _wheel;
		[SerializeField] private SpinWheelItemIndicator[] _wheelItemIndicators;

		private const float _spinWheelItemStepAngle = 360f / SpinWheelSystemLogicConfiguration.SpinWheelItemsCount;
		
		private Sequence _spinSequence;

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

		public void UpdateWheelView(Image wheelImage, Image pinImage)
		{
			_pin = pinImage;
			_wheel = wheelImage;
		}

		public void UpdateWheelItems(SpinWheelItemDto[] spinWheelItems)
		{
			for (int i = 0; i < _wheelItemIndicators.Length; i++)
			{
				_wheelItemIndicators[i].SetItemIdAndAmount(spinWheelItems[i]);
			}
		}

		public void ResetElement()
		{
			_pin.rectTransform.localRotation = Quaternion.identity;
			_wheel.rectTransform.localRotation = Quaternion.identity;
		}

		private void OnDestroy()
		{
			_spinSequence?.Kill(true);
		}
	}
}

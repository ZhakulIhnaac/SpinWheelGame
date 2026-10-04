using System;
using DG.Tweening;
using Game.SharedGameSystems.HapticSystem.Controllers;
using Game.SharedGameSystems.SoundSystem.Scripts.Controllers;
using Game.SharedGameSystems.SoundSystem.Scripts.Data;
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

		private const float _spinWheelItemStepAngle = 360f / SpinWheelSystemLogicConfiguration.SpinWheelSliceCount;
		private const float _pinMaxRotation = 15f;
		private const float _pinPushStartRatio = 0.6f;
		
		private Sequence _spinSequence;
		private Sequence _zoneChangeSequence;
		private Vector2 _originalAnchoredPosition;
		private float _pinEffectCountdownTime;
		private int _lastPassedPegIndex;

		public void Initialize()
		{
			_originalAnchoredPosition = RectTransform.anchoredPosition;

			foreach (var wheelItemIndicator in _wheelItemIndicators)
			{
				wheelItemIndicator.Initialize();
			}

			ResetElement();
		}

		private void Update()
		{
			HandlePinEffect();
		}

		private void HandlePinEffect()
		{
			var angleSinceFirstPeg = Mathf.Repeat(-_wheel.rectTransform.localEulerAngles.z - _spinWheelItemStepAngle * 0.5f, 360f);
			var pegIndex = Mathf.FloorToInt(angleSinceFirstPeg / _spinWheelItemStepAngle);
			var progressToNextPeg = angleSinceFirstPeg / _spinWheelItemStepAngle - pegIndex;
			var pushRatio = Mathf.Clamp01((progressToNextPeg - _pinPushStartRatio) / (1f - _pinPushStartRatio));
			_pin.rectTransform.localRotation = Quaternion.Euler(0f, 0f, pushRatio * _pinMaxRotation);

			_pinEffectCountdownTime = Mathf.Max(-0.5f, _pinEffectCountdownTime - Time.deltaTime);

			if (pegIndex == _lastPassedPegIndex) return;

			_lastPassedPegIndex = pegIndex;

			if (!_spinSequence.IsActive() || _pinEffectCountdownTime >= 0f) return;

			_pinEffectCountdownTime = 0.05f;
			SoundSystemManager.Instance.PlaySoundEffectOnce(SpinWheelSystemManager.Instance.WheelPinTickSound);
			HapticSystemsManager.Instance.PlayLightHaptic();
		}

		public Tween GetOpeningAnimation()
		{
			RectTransform.anchoredPosition = new Vector2(_originalAnchoredPosition.x, -Screen.height);
			return RectTransform.DOAnchorPosY(_originalAnchoredPosition.y, 0.6f)
								.SetEase(Ease.OutCubic);
		}

		public void SpinWheelToTheItem(int itemNumber)
		{
			var totalSpinAngle = 20 + 360f * 10f + (360f - (itemNumber - 1) * _spinWheelItemStepAngle);

			_spinSequence?.Kill();
			_spinSequence = DOTween.Sequence();

			_spinSequence.Append
				(
					transform.DOScale(Vector3.one * 1.2f, 0.2f)
				 );
			
			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, 20), 0.3f)
					   .SetRelative(true)
				);

			_spinSequence.Append
				(
				 _wheel.rectTransform.DOLocalRotate(new Vector3(0, 0, -totalSpinAngle), SpinWheelSystemManager.WheelSpinTime - 0.7f)
					   .SetRelative(true)
					   .SetEase(Ease.OutQuart)
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
				 RectTransform.DOAnchorPosY(_originalAnchoredPosition.y, 0.3f)
								  .SetEase(Ease.InSine)
				);

			_zoneChangeSequence.Play();
		}

		public void UpdateForCurrentZone()
		{
			_wheel.transform.rotation = Quaternion.identity;
			var wheelDisplaySlices = SpinWheelSystemManager.Instance.DisplayingSlices;
			
			for (int i = 0; i < _wheelItemIndicators.Length; i++)
			{
				_wheelItemIndicators[i].SetSlice(wheelDisplaySlices[i]);
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

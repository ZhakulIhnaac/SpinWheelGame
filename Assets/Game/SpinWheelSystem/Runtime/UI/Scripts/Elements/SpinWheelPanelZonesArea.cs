using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelZonesArea : MonoBehaviour
	{
		[SerializeField] private RectTransform _zonesListContent;
		[SerializeField] private Image _currentZoneIndicatorBackgroundLeft;
		[SerializeField] private Image _currentZoneIndicatorBackgroundRight;

		private Sequence _moveToTheNextZoneSequence;

		public void Initialize()
		{
			PopulateZoneIndicators();
			ResetElement();
		}

		public void MoveToTheNextZone(SpinZoneId nextSpinZoneId)
		{
			_moveToTheNextZoneSequence?.Kill(true);

			_moveToTheNextZoneSequence = DOTween.Sequence();

			_moveToTheNextZoneSequence.OnStart(() =>
											   {
												   _currentZoneIndicatorBackgroundRight.color = SpinWheelSystemManager.Instance.GetZoneSpecification(nextSpinZoneId).ZoneIndicatorBackgroundColor;
											   }
											  );

			_moveToTheNextZoneSequence.Append(
											  _zonesListContent.DOAnchorPosX(SpinWheelSystemManager.Instance.ZoneStepWidth, SpinWheelSystemManager.ZoneStepTime)
															   .SetEase(Ease.InOutSine)
											 );

			_moveToTheNextZoneSequence.Join(
											_currentZoneIndicatorBackgroundLeft.rectTransform.DOScale(new Vector3(0f, 1f, 1f), SpinWheelSystemManager.ZoneStepTime / 2f)
																			   .SetEase(Ease.InOutSine)
										   );

			_moveToTheNextZoneSequence.Join(
											_currentZoneIndicatorBackgroundRight.rectTransform.DOScale(new Vector3(1f, 1f, 1f), SpinWheelSystemManager.ZoneStepTime / 2f)
																				.SetEase(Ease.InOutSine)
										   );

			_moveToTheNextZoneSequence.OnComplete(() =>
												  {
													  _currentZoneIndicatorBackgroundLeft.rectTransform.localScale = new Vector3(1f, 1f, 1f);
													  _currentZoneIndicatorBackgroundLeft.color = _currentZoneIndicatorBackgroundRight.color;
													  _currentZoneIndicatorBackgroundRight.rectTransform.localScale = new Vector3(0f, 1f, 1f);
												  }
												 );

			_moveToTheNextZoneSequence.Play();
		}

		private void PopulateZoneIndicators()
		{
			var zonesCount = SpinWheelSystemManager.Instance.GetZoneCount();
			var zoneIndicatorPrefab = SpinWheelSystemManager.Instance.GetZoneIndicatorPrefab();

			for (int i = 0; i < zonesCount; i++)
			{
				var zoneNumber = i + 1;
				var newIndicator = Instantiate(zoneIndicatorPrefab, _zonesListContent);
				newIndicator.Initialize(SpinWheelSystemManager.Instance.GetSpinZoneForNumber(zoneNumber), zoneNumber);
			}
		}

		public void ResetElement()
		{
			_zonesListContent.anchoredPosition = Vector2.zero;
			_currentZoneIndicatorBackgroundLeft.color = Color.white;
			_currentZoneIndicatorBackgroundLeft.rectTransform.localScale = new Vector3(1, 1, 1);
			_currentZoneIndicatorBackgroundRight.color = Color.white;
			_currentZoneIndicatorBackgroundRight.rectTransform.localScale = new Vector3(0, 1, 1);
		}

		private void OnDestroy()
		{
			_moveToTheNextZoneSequence?.Kill(true);
		}
	}
}

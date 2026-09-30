using System.Collections.Generic;
using AssetKits.ParticleImage;
using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelRewardsArea : MonoBehaviour
	{
		[field: SerializeField] public RectTransform RectTransform;
		[SerializeField] private RectTransform _rewardsListContent;
		[SerializeField] private ParticleImage _resourceCollectParticle;

		private readonly Dictionary<SpinWheelItemId, SpinWheelPanelRewardIndicator> _rewardIndicators = new(16);

		private Transform _wheelElementEarnedRewardPosition;
		private Vector2 _originalAnchoredPosition;
		
		public void Initialize(Transform wheelElementEarnedRewardPosition)
		{
			_wheelElementEarnedRewardPosition = wheelElementEarnedRewardPosition;
			_originalAnchoredPosition = RectTransform.anchoredPosition;
		}

		public Tween GetOpeningAnimation()
		{
			RectTransform.anchoredPosition = new Vector2(-_originalAnchoredPosition.x, _originalAnchoredPosition.y);
			return RectTransform.DOAnchorPosX(_originalAnchoredPosition.x, 0.4f)
								.SetEase(Ease.OutCubic);
		}

		public void PlayRewardEarnAnimation()
		{
			var itemEarned = SpinWheelSystemManager.Instance.LastItemEarned;
			var indicator = _rewardIndicators.TryGetValue(itemEarned.SpinWheelItemId, out SpinWheelPanelRewardIndicator value) ? value : CreateNewRewardIndicator(itemEarned.SpinWheelItemId);
			var itemSpecification = SpinWheelSystemManager.Instance.GetItemSpecification(itemEarned.SpinWheelItemId);
			
			_resourceCollectParticle.transform.position = _wheelElementEarnedRewardPosition.position;
			_resourceCollectParticle.attractorTarget = indicator.AttractorTargetPosition;
			_resourceCollectParticle.sprite = itemSpecification.Icon;
			_resourceCollectParticle.rateOverLifetime = Mathf.Clamp(itemEarned.Amount, 1, 5);
			_resourceCollectParticle.onFirstParticleFinished.AddListener(DoOnFirstParticleFinished);
			_resourceCollectParticle.onAnyParticleFinished.AddListener(DoOnAnyParticleFinished);

			_resourceCollectParticle.Play();

			void DoOnFirstParticleFinished()
			{
				indicator.PlayAmountUpdateAnimation();
			}

			void DoOnAnyParticleFinished()
			{
				indicator.PlayItemAddedEffect();
			}
		}
		
		private SpinWheelPanelRewardIndicator CreateNewRewardIndicator(SpinWheelItemId itemEarnedSpinWheelItemId)
		{
			var newIndicator = Instantiate(SpinWheelSystemManager.Instance.GetRewardIndicatorPrefab(), _rewardsListContent);
			newIndicator.Initialize(itemEarnedSpinWheelItemId);
			_rewardIndicators.Add(itemEarnedSpinWheelItemId, newIndicator);
			return newIndicator;
		}

		public void ResetElement()
		{
			foreach (var indicator in _rewardIndicators.Values)
			{
				Destroy(indicator.gameObject);
			}

			_resourceCollectParticle.Stop();
			_rewardIndicators.Clear();
		}
	}
}

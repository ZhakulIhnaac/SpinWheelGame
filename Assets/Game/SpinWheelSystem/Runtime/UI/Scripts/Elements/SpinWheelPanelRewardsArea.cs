using System.Collections.Generic;
using AssetKits.ParticleImage;
using DG.Tweening;
using Game.InventorySystem.Runtime.Scripts.Data;
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

		private readonly Dictionary<ItemId, SpinWheelPanelRewardIndicator> _rewardIndicators = new(16);

		private Transform _wheelElementEarnedRewardPosition;
		private Vector2 _originalAnchoredPosition;
		private SpinWheelPanelRewardIndicator _collectTargetIndicator;
		
		public void Initialize(Transform wheelElementEarnedRewardPosition)
		{
			_wheelElementEarnedRewardPosition = wheelElementEarnedRewardPosition;
			_originalAnchoredPosition = RectTransform.anchoredPosition;
			_resourceCollectParticle.onFirstParticleFinished.AddListener(OnFirstParticleFinished);
			_resourceCollectParticle.onAnyParticleFinished.AddListener(OnAnyParticleFinished);
		}

		public Tween GetOpeningAnimation()
		{
			RectTransform.anchoredPosition = new Vector2(-_originalAnchoredPosition.x, _originalAnchoredPosition.y);
			return RectTransform.DOAnchorPosX(_originalAnchoredPosition.x, 0.4f)
								.SetEase(Ease.OutCubic);
		}

		public void PlayRewardEarnAnimation()
		{
			var itemEarned = SpinWheelSystemManager.Instance.LastSpinResult;
			_collectTargetIndicator = _rewardIndicators.TryGetValue(itemEarned.ItemId, out SpinWheelPanelRewardIndicator value) ? value : CreateNewRewardIndicator(itemEarned.ItemId);
			
			_resourceCollectParticle.transform.position = _wheelElementEarnedRewardPosition.position;
			_resourceCollectParticle.attractorTarget = _collectTargetIndicator.AttractorTargetPosition;
			_resourceCollectParticle.sprite = SpinWheelSystemManager.Instance.GetItemIcon(itemEarned.ItemId);
			_resourceCollectParticle.rateOverLifetime = Mathf.Clamp(itemEarned.Amount, 1, 5);

			_resourceCollectParticle.Play();
		}

		private void OnFirstParticleFinished() => _collectTargetIndicator.PlayAmountUpdateAnimation();

		private void OnAnyParticleFinished() => _collectTargetIndicator.PlayItemAddedEffect();
		
		private SpinWheelPanelRewardIndicator CreateNewRewardIndicator(ItemId itemId)
		{
			var newIndicator = Instantiate(SpinWheelSystemManager.Instance.GetRewardIndicatorPrefab(), _rewardsListContent);
			newIndicator.Initialize(itemId);
			_rewardIndicators.Add(itemId, newIndicator);
			return newIndicator;
		}

		public void ResetElement()
		{
			foreach (var indicator in _rewardIndicators.Values)
			{
				Destroy(indicator.gameObject);
			}

			_resourceCollectParticle.Stop();
			_resourceCollectParticle.Clear();
			_rewardIndicators.Clear();
			_collectTargetIndicator = null;
		}
	}
}

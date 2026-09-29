using System.Collections.Generic;
using AssetKits.ParticleImage;
using AssetKits.ParticleImage.Enumerations;
using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelRewardsArea : MonoBehaviour
	{
		[SerializeField] private RectTransform _rewardsListContent;
		[SerializeField] private ParticleImage _resourceCollectParticle;

		private readonly Dictionary<SpinWheelItemId, SpinWheelPanelRewardIndicator> _rewardIndicators = new(16);
		
		public void Initialize(Transform wheelElementEarnedRewardPosition)
		{
			_resourceCollectParticle.transform.position = wheelElementEarnedRewardPosition.position;
		}

		public void PlayRewardEarnAnimation()
		{
			var itemEarned = SpinWheelSystemManager.Instance.LastItemEarned;
			var indicator = _rewardIndicators.TryGetValue(itemEarned.SpinWheelItemId, out SpinWheelPanelRewardIndicator value) ? value : CreateNewRewardIndicator(itemEarned.SpinWheelItemId);
			var itemSpecification = SpinWheelSystemManager.Instance.GetItemSpecification(itemEarned.SpinWheelItemId);
			
			_resourceCollectParticle.attractorTarget = indicator.AttractorTargetPosition;
			_resourceCollectParticle.sprite = itemSpecification.Icon;
			_resourceCollectParticle.rateOverLifetime = Mathf.Clamp(itemEarned.Amount, 1, 10);
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
			return newIndicator;
		}

		public void ResetElement()
		{
			foreach (var indicator in _rewardIndicators.Values)
			{
				Destroy(indicator.gameObject);
			}
		
			_rewardIndicators.Clear();
		}
	}
}

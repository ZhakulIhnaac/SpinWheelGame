using System.Collections.Generic;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelRewardsArea : MonoBehaviour
	{
		[SerializeField] private RectTransform _rewardsListContent;

		private List<SpinWheelPanelRewardIndicator> _rewardIndicators = new(16);
		
		public void Initialize()
		{
		}
		
		public void AddReward()
		{
		}

		public void ResetElement()
		{
			for (int i = 0; i < _rewardIndicators.Count; i++)
			{
				Destroy(_rewardIndicators[i].gameObject);
			}
			
			_rewardIndicators.Clear();
		}
	}
}

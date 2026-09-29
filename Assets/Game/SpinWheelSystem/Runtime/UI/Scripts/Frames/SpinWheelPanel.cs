using System.Collections;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class SpinWheelPanel : MonoBehaviour
{
	[SerializeField] private SpinWheelPanelRewardsArea _rewardsArea;
	[SerializeField] private SpinWheelPanelZonesArea _zonesArea;
	[SerializeField] private SpinWheelPanelWheelElement _wheelElement;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _spinButton;

	private Coroutine _lockInteractionCoroutine;
	private Coroutine _spinAnimationCoroutine;

	public void Initialize()
	{
		_wheelElement.Initialize();
		_zonesArea.Initialize();
		_rewardsArea.Initialize(_wheelElement.EarnedRewardPosition);
		_exitButton.onClick.AddListener(OnExitButtonClicked);
		_spinButton.onClick.AddListener(OnSpinButtonClicked);
		SetNewRewards();
		ClosePanel();
	}

	public void OpenPanel()
	{
		_rewardsArea.ResetElement();
		_zonesArea.ResetElement();
		_wheelElement.ResetElement();
		gameObject.SetActive(true);
	}

	private void OnSpinButtonClicked()
	{
		if (_spinAnimationCoroutine != null) StopCoroutine(_spinAnimationCoroutine);

		_spinAnimationCoroutine = StartCoroutine(SpinAnimationCoroutine());

		IEnumerator SpinAnimationCoroutine()
		{
			LockInteractionForTime(SpinWheelSystemManager.WheelSpinTime + SpinWheelSystemManager.RewardGiveAnimationTime);
			_wheelElement.SpinWheelToTheItem(SpinWheelSystemManager.Instance.GetRewardNumberForSpinningTheWheel());
			yield return new WaitForSeconds(SpinWheelSystemManager.WheelSpinTime);
			_rewardsArea.PlayRewardEarnAnimation();
		}
	}

	private void OnExitButtonClicked()
	{
		SpinWheelSystemManager.Instance.AddEarnedItemsIntoInventory();
		ClosePanel();
	}

	private void ClosePanel()
	{
		gameObject.SetActive(false);
	}

	#region Utils
	private void LockInteractionForTime(float time)
	{
		if (_lockInteractionCoroutine != null) StopCoroutine(_lockInteractionCoroutine);

		_lockInteractionCoroutine = StartCoroutine(LockInteractionCoroutine());

		IEnumerator LockInteractionCoroutine()
		{
			ToggleInteraction(false);
			yield return new WaitForSeconds(time);
			ToggleInteraction(true);
		}

		void ToggleInteraction(bool isInteractable)
		{
			_exitButton.interactable = isInteractable;
			_spinButton.interactable = isInteractable;
		}
	}

	private void SetNewRewards()
	{
		_wheelElement.UpdateWheelItems(SpinWheelSystemManager.Instance.GetItemsForSpinWheel());
	}
	#endregion
}

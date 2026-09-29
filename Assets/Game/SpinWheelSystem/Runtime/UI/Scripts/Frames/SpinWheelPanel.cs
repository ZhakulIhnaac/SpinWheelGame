using System.Collections;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelPanel : MonoBehaviour
{
	[SerializeField] private SpinWheelPanelRewardsArea _rewardsArea;
	[SerializeField] private SpinWheelPanelZonesArea _zonesArea;
	[SerializeField] private SpinWheelPanelWheelElement _wheelElement;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _spinButton;

	private Coroutine _lockInteractionCoroutine;
	
	public void Initialize()
	{
		_rewardsArea.Initialize();
		_zonesArea.Initialize();
		_wheelElement.Initialize();
		_exitButton.onClick.AddListener(OnExitButtonClicked);
		_spinButton.onClick.AddListener(OnSpinButtonClicked);
	}

	public void OpenPanel()
	{
		_rewardsArea.ResetElement();
		_zonesArea.ResetElement();
		_wheelElement.ResetElement();
		gameObject.SetActive(true);
	}

	private void ClosePanel()
	{
		gameObject.SetActive(false);
	}

	private void OnExitButtonClicked()
	{
		SpinWheelSystemManager.Instance.AddEarnedItemsIntoInventory();
		ClosePanel();
	}

	private void OnSpinButtonClicked()
	{
		_wheelElement.SpinWheelToTheItem(SpinWheelSystemManager.Instance.GetRewardNumberForSpinningTheWheel());
		LockInteractionForTime(SpinWheelSystemManager.WheelSpinTime + SpinWheelSystemManager.RewardGiveAnimationTime);
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
	#endregion
}

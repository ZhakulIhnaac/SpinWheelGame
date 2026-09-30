using System.Collections;
using DG.Tweening;
using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelPanel : MonoBehaviour
{
	[SerializeField] private SpinWheelPanelBombOverlayElement _bombOverlayElement;
	[SerializeField] private SpinWheelPanelRewardsArea _rewardsArea;
	[SerializeField] private SpinWheelPanelZonesArea _zonesArea;
	[SerializeField] private SpinWheelPanelWheelElement _wheelElement;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _spinButton;

	private Coroutine _lockInteractionCoroutine;
	private Coroutine _spinAnimationCoroutine;
	private Sequence _openingAnimationSequence;
	private int _inputLockCounter;

	public void Initialize()
	{
		_bombOverlayElement.Closed += OnBombOverlayClosed;

		_bombOverlayElement.Initialize();
		_wheelElement.Initialize();
		_zonesArea.Initialize();
		_rewardsArea.Initialize(_wheelElement.EarnedRewardPosition);
		_exitButton.onClick.AddListener(OnExitButtonClicked);
		_spinButton.onClick.AddListener(OnSpinButtonClicked);
		ClosePanel();
	}

	public void OpenPanel()
	{
		_rewardsArea.ResetElement();
		_zonesArea.ResetElement();
		_wheelElement.ResetElement();
		_wheelElement.UpdateForCurrentZone();
		gameObject.SetActive(true);
		UpdateExitButtonAvailability();
		PlayOpeningAnimation();
	}

	private void PlayOpeningAnimation()
	{
		_openingAnimationSequence?.Kill();
		_openingAnimationSequence = DOTween.Sequence();

		_openingAnimationSequence.Append
			(
			 _rewardsArea.GetOpeningAnimation()
			);

		_openingAnimationSequence.Join
			(
			 _zonesArea.GetOpeningAnimation()
			);

		_openingAnimationSequence.Join
			(
			 _wheelElement.GetOpeningAnimation()
			);

		_openingAnimationSequence.Play();
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
			HandleSpinResult();
		}

		void HandleSpinResult()
		{
			if (SpinWheelSystemManager.Instance.LastItemEarned.SpinWheelItemId != SpinWheelItemId.Bomb)
			{
				_rewardsArea.PlayRewardEarnAnimation();
				TryToAdvanceToTheNextZone();
			}
			else
			{
				ToggleInteraction(false);
				_bombOverlayElement.Open();
			}
		}
	}

	private void OnExitButtonClicked()
	{
		SpinWheelSystemManager.Instance.DoOnSpinWheelPanelClosing(true);
		ClosePanel();
	}

	private void OnBombOverlayClosed(bool didGiveUp)
	{
		ToggleInteraction(true);

		if (didGiveUp)
		{
			SpinWheelSystemManager.Instance.DoOnSpinWheelPanelClosing(false);
			ClosePanel();
		}
		else
		{
			TryToAdvanceToTheNextZone();
		}
	}

	private void TryToAdvanceToTheNextZone()
	{
		if (SpinWheelSystemManager.Instance.TryToAdvanceToTheNextZone())
		{
			UpdateExitButtonAvailability();
			SpinWheelSystemManager.Instance.SetNewItemsForCurrentZone();
			_zonesArea.MoveToTheCurrentZone();
			_wheelElement.PlayUpdateWithZoneChangeAnimation();
		}
		else
		{
			SpinWheelSystemManager.Instance.DoOnSpinWheelPanelClosing(true);
			ClosePanel();
		}
	}

	private void UpdateExitButtonAvailability() => _exitButton.gameObject.SetActive(SpinWheelSystemManager.Instance.CurrentZoneId != SpinZoneId.Basic);

	private void ClosePanel()
	{
		_rewardsArea.ResetElement();
		_zonesArea.ResetElement();
		_wheelElement.ResetElement();
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
	}

	void ToggleInteraction(bool isInteractable)
	{
		_inputLockCounter += isInteractable ? 1 : -1;

		_exitButton.interactable = _inputLockCounter == 0;
		_spinButton.interactable = _inputLockCounter == 0;
	}
	#endregion
	
	public void TestOpenBombOverlay()
	{
		ToggleInteraction(false);
		_bombOverlayElement.Open();
	}
}

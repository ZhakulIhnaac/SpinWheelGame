using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelBombOverlayElement : MonoBehaviour
	{
		public event ClosedDelegate Closed;
		
		[SerializeField] private Image _backgroundDarkness;
		[SerializeField] private Image _bombIcon;
		[SerializeField] private Image _deathShine;
		[SerializeField] private Button _giveUpButton;
		[SerializeField] private Button _reviveButton;

		private Sequence _toggleSequence;
		
		public void Initialize()
		{
			_reviveButton.onClick.AddListener(OnSaveButtonClicked);
			_giveUpButton.onClick.AddListener(OnGiveUpButtonClicked);
			gameObject.SetActive(false);
			_backgroundDarkness.color = new Color(0f, 0f, 0f, 0f);
			ToggleInteraction(false);
		}
		
		public void Open()
		{
			ToggleInteraction(false);
			gameObject.SetActive(true);
			_backgroundDarkness.color = new Color(0f, 0f, 0f, 0f);
			_bombIcon.color = new Color(1f, 1f, 1f, 0f);
			_bombIcon.transform.localScale = Vector3.one * 3f;

			_toggleSequence?.Kill(true);

			_toggleSequence = DOTween.Sequence();

			_toggleSequence.Append
				(
				 _backgroundDarkness.DOFade(0.95f, 0.3f)
				);

			_toggleSequence.Append
				(
				 _bombIcon.DOFade(1f, 0.5f)
				);

			_toggleSequence.Join
				(
				 _bombIcon.transform.DOScale(1f, 0.5f)
				);

			_toggleSequence.Append
				(
				 _deathShine.DOFade(0.9f, 0.1f)
							.SetEase(Ease.Linear)
				);

			_toggleSequence.Append
				(
				 _deathShine.DOFade(0f, 1.5f)
							.SetEase(Ease.Linear)
				);
			
			_toggleSequence.OnComplete(() => ToggleInteraction(true));

			_toggleSequence.Play();
		}

		private void Close(bool didGiveUp)
		{
			ToggleInteraction(false);
			
			_toggleSequence?.Kill(true);

			_toggleSequence = DOTween.Sequence();

			_toggleSequence.Append
				(
					_bombIcon.transform.DOScale(0f, 1f)
							 .SetEase(Ease.InBack)
				);

			_toggleSequence.Join
				(
				 _bombIcon.transform.DORotate(new Vector3(0f, 360f * 5f, 0f), 1f)
						  .SetEase(Ease.InOutSine)
				);
			
			_toggleSequence.OnComplete(() =>
									   {
										   gameObject.SetActive(false);
										   Closed?.Invoke(didGiveUp);
									   }
									  );
			
			_toggleSequence.Play();
		}

		private void OnSaveButtonClicked()
		{
			Close(false);
		}

		private void OnGiveUpButtonClicked()
		{
			Close(true);
		}

		private void ToggleInteraction(bool isEnabled)
		{
			_giveUpButton.interactable = isEnabled;
			_reviveButton.interactable = isEnabled;
		}
		
		private void OnDestroy()
		{
			_toggleSequence?.Kill(true);
		}

		#region Delegates
		public delegate void ClosedDelegate(bool didGiveUp);
		#endregion
	}
}

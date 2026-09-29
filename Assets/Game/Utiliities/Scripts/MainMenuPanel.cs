using Game.SpinWheelSystem.Runtime.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
	[SerializeField] private Button _openSpinWheelButton;

	public void Initialize()
	{
		_openSpinWheelButton.onClick.AddListener(OnOpenSpinWheelButtonClicked);
	}

	private void OnOpenSpinWheelButtonClicked()
	{
		SpinWheelSystemManager.Instance.OpenSpinWheelPanel();
	}
}

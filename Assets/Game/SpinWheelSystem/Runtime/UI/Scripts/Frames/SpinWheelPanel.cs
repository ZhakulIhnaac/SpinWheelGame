using Game.SpinWheelSystem.Runtime.UI.Scripts.Elements;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelPanel : MonoBehaviour
{
	[SerializeField] private SpinWheelPanelRewardsArea _rewardsArea;
	[SerializeField] private SpinWheelPanelZonesArea _zonesArea;
	[SerializeField] private SpinWheelPanelWheelElement _wheel;
	[SerializeField] private Button _exitButton;

	public void Initialize()
	{
		_rewardsArea.Initialize();
		_zonesArea.Initialize();
		_wheel.Initialize();
		_exitButton.onClick.AddListener(OnExitButtonClicked);
	}

	private void OnExitButtonClicked()
	{
		
	}
}

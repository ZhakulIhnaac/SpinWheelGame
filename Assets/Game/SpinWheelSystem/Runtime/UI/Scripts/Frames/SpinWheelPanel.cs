using UnityEngine;
using UnityEngine.UI;

public class SpinWheelPanel : MonoBehaviour
{
	[SerializeField] private Button _exitButton;

	public void Initialize()
	{
		_exitButton.onClick.AddListener(OnExitButtonClicked);
	}

	private void OnExitButtonClicked()
	{
		
	}
}

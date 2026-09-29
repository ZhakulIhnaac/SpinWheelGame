using Game.SpinWheelSystem.Runtime.Scripts;
using UnityEngine;

namespace Utils.Singleton
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private MainMenuPanel _mainMenuPanel;
		
		private void Awake()
		{
			_mainMenuPanel.Initialize();
			SpinWheelSystemManager.Instance.Initialize();
		}
	}
}

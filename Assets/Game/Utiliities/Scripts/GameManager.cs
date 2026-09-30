using Game.InventorySystem.Runtime.Scripts;
using Game.SharedGameSystems.HapticSystem.Controllers;
using Game.SharedGameSystems.SoundSystem.Scripts.Controllers;
using Game.SpinWheelSystem.Runtime.Scripts;
using UnityEngine;

namespace Utilities
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private MainMenuPanel _mainMenuPanel;
		
		private void Awake()
		{
			Application.targetFrameRate = 60;
			_mainMenuPanel.Initialize();
			HapticSystemsManager.Instance.Initialize();
			SoundSystemManager.Instance.Initialize();
			SpinWheelSystemManager.Instance.Initialize();
			InventorySystemManager.Instance.Initialize();
		}
	}
}

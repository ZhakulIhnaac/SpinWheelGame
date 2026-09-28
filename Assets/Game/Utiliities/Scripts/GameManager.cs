using UnityEngine;

namespace Utils.Singleton
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private SpinWheelPanel _spinWheelPanel;
		
		private void Awake()
		{
			_spinWheelPanel.Initialize();
		}
	}
}

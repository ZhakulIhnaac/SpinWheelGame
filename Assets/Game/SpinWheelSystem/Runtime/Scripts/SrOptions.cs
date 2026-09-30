using System.ComponentModel;
using Game.SpinWheelSystem.Runtime.Scripts;

public partial class SROptions
{
	[Category("Spin Wheel System")]
	public void OpenBombOverlay()
	{
		SpinWheelSystemManager.Instance.TestOpenBombOverlay();
	}
}

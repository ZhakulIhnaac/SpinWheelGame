using Lofelt.NiceVibrations;
using Utils.Singleton;

namespace Game.SharedGameSystems.HapticSystem.Controllers
{
	public class HapticSystemsManager : SingletonMonoBehaviour<HapticSystemsManager>
	{
		public void Initialize() { }

		public void PlayLightHaptic() => HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
	}
}

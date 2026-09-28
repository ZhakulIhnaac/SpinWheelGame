using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelPanelZoneIndicator : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _zoneNumberText;
		[SerializeField] private RectTransform _selfRectTransform;
		public float Width => _selfRectTransform.rect.width;

		public void Initialize(SpinZoneId spinZoneId, int zoneNumber)
		{
			_zoneNumberText.SetText($"{zoneNumber}");
			_zoneNumberText.color = SpinWheelSystemManager.Instance.GetZoneSpecification(spinZoneId).ZoneTextColor;
		}
	}
}

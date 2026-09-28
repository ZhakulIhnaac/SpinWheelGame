using Game.SpinWheelSystem.Runtime.Scripts;
using Game.SpinWheelSystem.Runtime.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.SpinWheelSystem.Runtime.UI.Scripts.Elements
{
	public class SpinWheelItemIndicator : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _amountText;
		
		public void SetItemIdAndAmount(SpinWheelItemDto spinWheelItem)
		{
			var icon = SpinWheelSystemManager.Instance.GetItemSpecification(spinWheelItem.SpinWheelItem).Icon;
			_amountText.SetText($"{spinWheelItem.Amount}");
		}
	}
}

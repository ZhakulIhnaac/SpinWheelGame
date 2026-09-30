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
			_icon.sprite = SpinWheelSystemManager.Instance.GetItemSpecification(spinWheelItem.SpinWheelItemId).Icon;

			if (spinWheelItem.SpinWheelItemId == SpinWheelItemId.Bomb)
			{
				_amountText.SetText($"");
				_icon.rectTransform.anchoredPosition = Vector2.zero;
			}
			else
			{
				_amountText.SetText($"{spinWheelItem.Amount}");
			}
		}
	}
}

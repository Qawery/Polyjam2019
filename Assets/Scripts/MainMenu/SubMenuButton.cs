using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class SubMenuButton : MonoBehaviour
{
	[SerializeField] private GameObject menuToTurnOff;
	[SerializeField] private GameObject menuToTurnOn;


	private void Awake()
	{
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
		Assert.IsNotNull(menuToTurnOff, "Missing menuToTurnOff");
		Assert.IsNotNull(menuToTurnOn, "Missing menuToTurnOn");
	}

	private void OnClicked()
	{
		menuToTurnOff.SetActive(false);
		menuToTurnOn.SetActive(true);
	}
}

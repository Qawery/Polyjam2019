using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class QuitToMainMenuButton : MonoBehaviour
{
	private void Awake()
	{
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
	}

	private void OnClicked()
	{
		if (BaseState.Instance != null)
		{
			BaseState.Instance.Reset();
		}
		Application.LoadLevel("MainMenu");
	}
}

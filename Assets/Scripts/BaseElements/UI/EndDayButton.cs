using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class EndDayButton : MonoBehaviour
{
	private void Start()
	{
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
	}

	private void OnClicked()
	{
		FindObjectOfType< BaseControlFlowManager>().BeginDayEnd();
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class CurrentActionSelected : MonoBehaviour
{
	private Text text;


	private void Awake()
	{
		BaseState.Instance.OnActionChange += UpdateAction;
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		UpdateAction();
	}

	private void UpdateAction()
	{
		string newText = "Current action:\n";
		newText += BaseState.Instance.SelectedAction.Title;
		text.text = newText;
	}
}

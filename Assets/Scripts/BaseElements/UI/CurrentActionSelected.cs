using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class CurrentActionSelected : MonoBehaviour
{
	private BaseManager baseState;
	private Text text;


	private void Awake()
	{
		baseState = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseState, "Missing baseState");
		baseState.OnActionChange += UpdateAction;
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		UpdateAction();
	}

	private void UpdateAction()
	{
		string newText = "Current action:\n";
		newText += baseState.SelectedAction.Title;
		text.text = newText;
	}
}

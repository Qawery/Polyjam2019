using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class CurrentActionSelected : MonoBehaviour
{
	private BaseState baseState;
	private Text text;


	private void Awake()
	{
		baseState = FindObjectOfType<BaseState>();
		Assert.IsNotNull(baseState, "Missing baseState");
		baseState.OnActionChange += UpdateAction;
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		UpdateAction();
	}

	private void UpdateAction()
	{
		string newText = "Current action:\n";
		if (baseState.SelectedAction == null)
		{
			newText += "None";
		}
		else
		{
			newText += baseState.SelectedAction.Name;
		}
		text.text = newText;
	}
}

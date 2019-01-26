using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class SelectActionButton : MonoBehaviour
{
	private BaseState baseState;
	private Image image;
	private Text text;
	private Action action;


	private void Awake()
	{
		baseState = FindObjectOfType<BaseState>();
		Assert.IsNotNull(baseState, "Missing baseState");
		baseState.OnActionChange += UpdateButtonColor;
		baseState.OnResourcesChange += UpdateButtonColor;
		text = GetComponentInChildren<Text>();
		Assert.IsNotNull(text, "Missing text");
		image = GetComponent<Image>();
		Assert.IsNotNull(image, "Missing image");
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
		SetAction(action);
	}

	public void SetAction(Action newAction)
	{
		action = newAction;
		string newText;
		if (action == null)
		{
			newText = "Clear action";
		}
		else
		{
			newText = action.Name + "\nCost: ";
			foreach (var resourceEntry in action.ActionCost)
			{
				newText += resourceEntry.Key.ToString() + ": " + resourceEntry.Value.ToString() + "; ";
			}
		}
		text.text = newText;
		UpdateButtonColor();
	}

	private void UpdateButtonColor()
	{
		if (baseState.SelectedAction == action)
		{
			image.color = Color.yellow;
		}
		else if (action == null || baseState.HasResources(action.ActionCost))
		{
			image.color = Color.green;
		}
		else
		{
			image.color = Color.red;
		}
	}

	private void OnClicked()
	{
		baseState.SelectedAction = action;
	}
}

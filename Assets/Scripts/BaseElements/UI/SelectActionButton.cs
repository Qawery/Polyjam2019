using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class SelectActionButton : MonoBehaviour
{
	private BaseManager baseState;
	private Image image;
	private Text text;
	private Action action;


	private void Awake()
	{
		baseState = FindObjectOfType<BaseManager>();
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
		gameObject.SetActive(false);
	}

	public void SetAction(Action newAction)
	{
		action = newAction;
		if (action == null)
		{
			gameObject.SetActive(false);
		}
		else
		{
			string newText = action.Title;
			if (action.ActionCost.Count > 0)
			{
				newText += "\nCost: ";
				foreach (var resourceEntry in action.ActionCost)
				{
					newText += resourceEntry.Key.ToString() + ": " + resourceEntry.Value.ToString() + "; ";
				}
			}
			text.text = newText;
			UpdateButtonColor();
			gameObject.SetActive(true);
		}
	}

	private void UpdateButtonColor()
	{
		if (baseState.HasResources(action.ActionCost))
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

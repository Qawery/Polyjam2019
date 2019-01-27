using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class SelectActionButton : MonoBehaviour
{
	private Image image;
	private Text text;
	private Action action;


	private void Awake()
	{
		text = GetComponentInChildren<Text>();
		Assert.IsNotNull(text, "Missing text");
		image = GetComponent<Image>();
		Assert.IsNotNull(image, "Missing image");
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
		gameObject.SetActive(false);
		BaseState.Instance.OnResourceChange += UpdateButtonColor;
	}

	private void OnDestroy()
	{
		BaseState.Instance.OnResourceChange -= UpdateButtonColor;
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
			gameObject.SetActive(true);
			UpdateButtonColor();
		}
	}

	private void UpdateButtonColor()
	{
		if (BaseState.Instance.HasResources(action.ActionCost))
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
		BaseState.Instance.SelectedAction = action;
	}
}

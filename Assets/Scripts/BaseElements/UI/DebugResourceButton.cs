using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class DebugResourceButton : MonoBehaviour
{
	[SerializeField] private Resource resourceType;
	[SerializeField] private int value = 1;
	private Text text;
	private Image image;


	private void Start()
	{
		text = GetComponentInChildren<Text>();
		Assert.IsNotNull(text, "Missing text");
		image = GetComponent<Image>();
		Assert.IsNotNull(image, "Missing image");
		string newText = "";
		if (value >= 0)
		{
			newText = "Add " + value.ToString() + " " + ((Resource)resourceType).ToString();
		}
		else
		{
			newText = "Substract " + (-value).ToString() + " " + ((Resource)resourceType).ToString();
		}
		text.text = newText;
		Button button = GetComponent<Button>();
		Assert.IsNotNull(button, "Missing button");
		button.onClick.AddListener(OnClicked);
	}

	private void OnClicked()
	{
		BaseState.Instance.ChangeValuesOfResources(new Dictionary<Resource, int> { [resourceType] = value });
	}
}

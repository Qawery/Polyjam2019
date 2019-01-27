using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class DebugResourceButton : MonoBehaviour
{
	[SerializeField] private Resource resourceType;
	[SerializeField] private int value = 1;
	private BaseManager baseManager;
	private Text text;
	private Image image;


	private void Start()
	{
		baseManager = BaseManager.Instance;
		Assert.IsNotNull(baseManager, "Missing baseManager");
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
		baseManager.ChangeValuesOfResources(new Dictionary<Resource, int> { [resourceType] = value });
	}
}

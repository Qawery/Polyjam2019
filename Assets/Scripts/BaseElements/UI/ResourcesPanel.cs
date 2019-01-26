using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class ResourcesPanel : MonoBehaviour
{
	private BaseManager baseState;
	private Text text;


	private void Awake()
	{
		baseState = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseState, "Missing baseState");
		baseState.OnResourcesChange += UpdateResources;
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		UpdateResources();
	}

	private void UpdateResources()
	{
		string newText = "Resources:";
		for (int i = 0; i < (int)Resource.MAX; ++i)
		{
			newText += "\n" + ((Resource)i).ToString() + ": " + baseState.GetResourceQuantity((Resource)i);
		}
		text.text = newText;
	}
}

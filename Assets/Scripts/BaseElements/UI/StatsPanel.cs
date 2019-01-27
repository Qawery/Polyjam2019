using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class StatsPanel : MonoBehaviour
{
	private Text text;


	private void Start()
	{
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		BaseState.Instance.OnResourceChange += UpdateState;
		UpdateState();
	}

	private void OnDestroy()
	{
		BaseState.Instance.OnResourceChange -= UpdateState;
	}

	private void UpdateState()
	{
		string newText = "Days Left: " + BaseState.Instance.DaysLeft.ToString() + "\n";
		newText += "Resources:";
		for (int i = 0; i < (int)Resource.MAX; ++i)
		{
			newText += "\n" + ((Resource)i).ToString() + ": " + BaseState.Instance.GetResourceQuantity((Resource)i);
		}
		text.text = newText;
	}
}

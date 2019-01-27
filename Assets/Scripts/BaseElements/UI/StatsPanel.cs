using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class StatsPanel : MonoBehaviour
{
	private BaseManager baseManager;
	private Text text;


	private void Start()
	{
		baseManager = BaseManager.Instance;
		Assert.IsNotNull(baseManager, "Missing baseManager");
		baseManager.OnResourcesChange += UpdateState;
		baseManager.OnNewDayStart += UpdateState;
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		UpdateState();
	}

	private void UpdateState()
	{
		string newText = "Days Left: " + baseManager.DaysLeft.ToString() + "\n";
		newText += "Threat Level: " + baseManager.ThreatLevel + "\n";
		newText += "Resources:";
		for (int i = 0; i < (int)Resource.MAX; ++i)
		{
			newText += "\n" + ((Resource)i).ToString() + ": " + baseManager.GetResourceQuantity((Resource)i);
		}
		text.text = newText;
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class CurrentActionSelected : MonoBehaviour
{
	private BaseManager baseManager;
	private Text text;


	private void Start()
	{
		baseManager = BaseManager.Instance;
		Assert.IsNotNull(baseManager, "Missing baseManager");
		baseManager.OnActionChange += UpdateAction;
		text = GetComponent<Text>();
		Assert.IsNotNull(text, "Missing text");
		UpdateAction();
	}

	private void UpdateAction()
	{
		string newText = "Current action:\n";
		newText += baseManager.SelectedAction.Title;
		text.text = newText;
	}
}

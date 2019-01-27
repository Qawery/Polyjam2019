using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class ActionButtonsManager : MonoBehaviour
{
	[SerializeField] private SelectActionButton selectActionButtonPrefab;
	private BaseManager baseManager;
	private List<SelectActionButton> spawnedButtons = new List<SelectActionButton>();


	private void Start()
	{
		Assert.IsNotNull(selectActionButtonPrefab);
		baseManager = BaseManager.Instance;
		Assert.IsNotNull(baseManager, "Missing baseManager");
		baseManager.OnActionChange += UpdateActions;
		baseManager.OnPostActionResolve += UpdateActions;
		baseManager.OnNewDayStart += UpdateActions;
		UpdateActions();
	}

	private void UpdateActions()
	{
		List<Action> availableActions = baseManager.GetAvailableAction();
		while (spawnedButtons.Count > availableActions.Count)
		{
			spawnedButtons.RemoveAt(spawnedButtons.Count - 1);
		}
		while (spawnedButtons.Count < availableActions.Count)
		{
			SelectActionButton newButton = Instantiate(selectActionButtonPrefab);
			newButton.transform.parent = transform;
			spawnedButtons.Add(newButton);
		}
		int buttonIndex = 0;
		foreach (Action action in availableActions)
		{
			spawnedButtons[buttonIndex].SetAction(action);
			++buttonIndex;
		}
	}
}

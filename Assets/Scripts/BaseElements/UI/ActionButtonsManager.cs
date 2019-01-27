using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class ActionButtonsManager : MonoBehaviour
{
	[SerializeField] private SelectActionButton selectActionButtonPrefab;
	private List<SelectActionButton> spawnedButtons = new List<SelectActionButton>();


	private void Awake()
	{
		Assert.IsNotNull(selectActionButtonPrefab);
		BaseControlFlowManager.OnActionChange += UpdateActions;
		UpdateActions();
	}

	private void UpdateActions()
	{
		List<Action> availableActions = BaseState.Instance.GetAvailableAction();
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

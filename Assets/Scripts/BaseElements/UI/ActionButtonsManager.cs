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

	private void OnDestroy()
	{
		BaseControlFlowManager.OnActionChange -= UpdateActions;
	}

	private void UpdateActions()
	{
		int buttonIndex = 0;
		while (buttonIndex < spawnedButtons.Count)
		{
			if (spawnedButtons[buttonIndex] == null)
			{
				spawnedButtons.RemoveAt(buttonIndex);
			}
			else
			{
				++buttonIndex;
			}
		}
		List<Action> availableActions = BaseState.Instance.GetAvailableAction();
		while (spawnedButtons.Count > availableActions.Count)
		{
			spawnedButtons.RemoveAt(spawnedButtons.Count - 1);
		}
		while (spawnedButtons.Count < availableActions.Count)
		{
			SelectActionButton newButton = Instantiate(selectActionButtonPrefab);
			newButton.transform.SetParent(transform, false);
			spawnedButtons.Add(newButton);
		}
		buttonIndex = 0;
		foreach (Action action in availableActions)
		{
			spawnedButtons[buttonIndex].SetAction(action);
			++buttonIndex;
		}
	}
}

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class BaseManager : MonoBehaviour
{
	private Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
	private Dictionary<WorkstationType, Workstation> workstations = new Dictionary<WorkstationType, Workstation>();
	public System.Action OnResourcesChange;
	public System.Action OnActionChange;
	public System.Action OnDayEnd;
	public System.Action OnDayStart;
	private Action selectedAction = new NoneAction();
	public int DaysLeft { get; private set; } = 6;


	public Action SelectedAction 
	{
		get
		{
			return selectedAction;
		}

		set
		{
			if (selectedAction != null)
			{
				ChangeValuesOfResources(selectedAction.ActionCost);
			}
			if (value != null && HasResources(value.ActionCost))
			{
				selectedAction = value;
				ChangeValuesOfResources(selectedAction.ActionCost, true);
			}
			else
			{
				selectedAction = new NoneAction();
			}
			OnActionChange?.Invoke();
		}
	}


	private void Awake()
	{
		for (int i = 0; i < (int) Resource.MAX; ++i)
		{
			resources.Add((Resource) i, 0);
		}
		OnResourcesChange?.Invoke();
	}

	public void RegisterWorkstation(Workstation workstation)
	{
		Assert.IsNotNull(workstation, "Null workstation");
		Assert.IsFalse(workstations.ContainsKey(workstation.Id), "Duplicate key in workstationPrefabs");
		workstations.Add(workstation.Id, workstation);
	}

	public Workstation GetWorkstationOfType(WorkstationType workstationType)
	{
		if (workstations.ContainsKey(workstationType))
		{
			return workstations[workstationType];
		}
		else
		{
			return null;
		}
	}

	public List<Action> GetAvailableAction()
	{
		List<Action> result = new List<Action>();
		result.Add(new NoneAction());
		foreach (var workstation in workstations.Values)
		{
			if (workstation.CurrentLevel < workstation.MaxLevel)
			{
				result.Add(new UpgradeWorkstation(workstation));
			}
		}
		return result;
	}

	public void ChangeValueOfResource(Resource resouces, int value)
	{
		if (resources.ContainsKey(resouces))
		{
			resources[resouces] += value;
			if (resources[resouces] < 0)
			{
				resources[resouces] = 0;
			}
		}
		OnResourcesChange?.Invoke();
	}

	public void ChangeValuesOfResources(Dictionary<Resource, int> resoucesChange, bool negateValues = false)
	{
		foreach (var resourceChangeEntry in resoucesChange)
		{
			if (resources.ContainsKey(resourceChangeEntry.Key))
			{
				resources[resourceChangeEntry.Key] += negateValues ? -resourceChangeEntry.Value : resourceChangeEntry.Value;
				if (resources[resourceChangeEntry.Key] < 0)
				{
					resources[resourceChangeEntry.Key] = 0;
				}
			}
		}
		OnResourcesChange?.Invoke();
	}

	public bool HasResources(Dictionary<Resource, int> requestedResouces)
	{
		if (requestedResouces == null)
		{
			return false;
		}
		foreach (var requestedResourceEntry in requestedResouces)
		{
			if (!resources.ContainsKey(requestedResourceEntry.Key) || resources[requestedResourceEntry.Key] < requestedResourceEntry.Value)
			{
				return false;
			}
		}
		return true;
	}

	public int GetResourceQuantity(Resource resource)
	{
		if (resources.ContainsKey(resource))
		{
			return resources[resource];
		}
		else
		{
			return 0;
		}
	}

	public void EndDay()
	{
		Action executedAction = SelectedAction;
		SelectedAction = new NoneAction();
		ChangeValuesOfResources(executedAction.ActionCost, true);
		DaysLeft -= 1;
		OnDayEnd?.Invoke();
		//FIXME
		StartDay();
	}

	public void StartDay()
	{
		OnDayStart?.Invoke();
	}
}

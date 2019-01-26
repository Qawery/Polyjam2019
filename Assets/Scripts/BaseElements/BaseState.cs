using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class BaseState : MonoBehaviour
{
	private Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
	private Dictionary<WorkstationType, Workstation> workstations = new Dictionary<WorkstationType, Workstation>();
	public System.Action OnResourcesChange;
	public System.Action OnActionChange;
	private Action selectedAction = null;


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
				selectedAction = null;
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
}

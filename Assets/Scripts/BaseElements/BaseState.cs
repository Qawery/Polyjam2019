using UnityEngine;
using System.Collections.Generic;


public class BaseState : MonoBehaviour
{
	public System.Action OnActionChange;
	public System.Action OnResourceChange;
	private Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
	private Dictionary<WorkstationType, Workstation> workstations = new Dictionary<WorkstationType, Workstation>();
	private Action selectedAction = new Rest();
	public int DaysLeft { get; set; } = 30;
	public int ThreatLevel { get; set; } = 0;
	public bool wentToMission = false;


    private static BaseState instance = null;
    public static BaseState Instance
    {
        get
        {
            return instance;
        }

        private set
        {
            if(instance == null)
            {
                instance = value;
			}
        }
    }

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
				OnActionChange?.Invoke();
			}
			else
			{
				selectedAction = new Rest();
				OnActionChange?.Invoke();
			}
		}
	}


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Reset();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Reset()
	{
		selectedAction = new Rest();
		DaysLeft = 30;
		ThreatLevel = 0;
		wentToMission = false;
		resources.Clear();
		for (int i = 0; i < (int)Resource.MAX; ++i)
		{
			resources.Add((Resource)i, 0);
		}
		workstations.Clear();
		AlchemistTable alchemistTable = new AlchemistTable();
		workstations.Add(alchemistTable.Id, alchemistTable);
		Altar altar = new Altar();
		workstations.Add(altar.Id, altar);
		Garden garden = new Garden();
		workstations.Add(garden.Id, garden);
		Workshop workshop = new Workshop();
		workstations.Add(workshop.Id, workshop);
		//Startowe zasoby
		resources[Resource.Food] += 4;
		resources[Resource.Scrap] += 1;
		resources[Resource.Clues] += 1;
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
		result.Add(new Rest());
		result.Add(new Explore());
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
		OnResourceChange?.Invoke();
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
		OnResourceChange?.Invoke();
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
	
	//FIXME!!!
	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.LoadLevel("Base");
		}
	}
}

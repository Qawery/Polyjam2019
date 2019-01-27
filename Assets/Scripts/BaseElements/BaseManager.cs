using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class BaseManager : MonoBehaviour
{
	private GameEndConditions gameEndConditions;
	private FadeOutScreen dayEndAnimation;
	private Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
	private Dictionary<WorkstationType, Workstation> workstations = new Dictionary<WorkstationType, Workstation>();
	public System.Action OnResourcesChange;
	public System.Action OnActionChange;
	public System.Action OnPostActionResolve;
	public System.Action OnNewDayStart;
	private Action selectedAction = new NoneAction();
	public int DaysLeft { get; private set; } = 30;
	public int ThreatLevel { get; private set; } = 0;


    private static BaseManager instance = null;
    public static BaseManager Instance
    {
        get
        {
            return instance;
        }

        set
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
		if (Instance == null)
		{
			Instance = this;
			for (int i = 0; i < (int)Resource.MAX; ++i)
			{
				resources.Add((Resource)i, 0);
			}
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
			resources[Resource.Herbs] += 1;
			resources[Resource.Scrap] += 1;
			resources[Resource.Clues] += 1;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Base")
		{
			dayEndAnimation = Resources.FindObjectsOfTypeAll<FadeOutScreen>()[0];
			Assert.IsNotNull(dayEndAnimation, "Missing dayEndAnimation");
			gameEndConditions = FindObjectOfType<GameEndConditions>();
			Assert.IsNotNull(gameEndConditions, "Missing gameEndConditions");
			OnResourcesChange?.Invoke();
		}
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

	public void BeginDayEnd()
	{
		dayEndAnimation.gameObject.SetActive(true);
		dayEndAnimation.StartDarkening();
		dayEndAnimation.OnDarkeningEnd += BeginActionResolve;
	}

	private void BeginActionResolve()
	{
		dayEndAnimation.OnDarkeningEnd -= BeginActionResolve;
		Action executedAction = SelectedAction;
		SelectedAction = new NoneAction();
		ChangeValuesOfResources(executedAction.ActionCost, true);
		executedAction.Execute();   //TODO: odpalenie minigry chodzenia, po tym cos powinno odpalać PostActionResolve()
		PostActionResolve();
	}

	private void PostActionResolve()
	{
		//TODO: Rozważenie wyniku eksploracji
		dayEndAnimation.OnLighteningEnd += BeginNewDay;
		OnPostActionResolve?.Invoke();
	}

	public void BeginNewDay()
	{
		--DaysLeft;
		if (ThreatLevel > 0)
		{
			--ThreatLevel;
		}
		ChangeValueOfResource(Resource.Food, -1);
		dayEndAnimation.gameObject.SetActive(false);
		dayEndAnimation.OnLighteningEnd -= BeginNewDay;
		OnNewDayStart?.Invoke();
	}
}

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public class BaseState : MonoBehaviour
{
	[SerializeField] private Workstation[] workstationPrefabs;
	private Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
	private Dictionary<WorkstationType, Workstation> workstations = new Dictionary<WorkstationType, Workstation>();
	public System.Action OnResourcesChange;


	private void Awake()
	{
		for (int i = 0; i < (int) Resource.MAX; ++i)
		{
			resources.Add((Resource) i, 0);
		}
		foreach (var workstationPrefab in workstationPrefabs)
		{
			Assert.IsFalse(workstations.ContainsKey(workstationPrefab.Id), "Duplicate key in workstationPrefabs");
			workstations.Add(workstationPrefab.Id, workstationPrefab);
		}
		OnResourcesChange?.Invoke();
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

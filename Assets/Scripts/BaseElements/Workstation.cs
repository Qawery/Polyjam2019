using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;


public abstract class Workstation : MonoBehaviour
{
	public int CurrentLevel { get; protected set; } = 0;
	public abstract int MaxLevel { get; }
	public abstract WorkstationType Id { get; }
	public abstract string Name { get; }
	public abstract Dictionary<Resource, int> NextUpgradeCost { get; }


	protected void Awake()
	{
		BaseManager baseState;
		baseState = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseState, "Missing baseState");
		baseState.RegisterWorkstation(this);
	}

	public void UpgradeWorkstation()
	{
		if (CurrentLevel < MaxLevel)
		{
			++CurrentLevel;
		}
	}
}
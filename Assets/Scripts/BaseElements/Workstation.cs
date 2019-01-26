using UnityEngine;
using System.Collections.Generic;


public abstract class Workstation : MonoBehaviour
{
	public int CurrentLevel { get; protected set; } = 0;
	public abstract int MaxLevel { get; }
	public abstract WorkstationType Id { get; }
	public abstract string Name { get; }


	public abstract List<KeyValuePair<Resource, int>> NextUpgradeCost();
}
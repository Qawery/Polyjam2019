using System.Collections.Generic;


public abstract class Workstation
{
	public int CurrentLevel { get; protected set; } = 1;
	public abstract int MaxLevel { get; }
	public abstract WorkstationType Id { get; }
	public abstract string Name { get; }
	public abstract Dictionary<Resource, int> NextUpgradeCost { get; }


	public void UpgradeWorkstation()
	{
		if (CurrentLevel < MaxLevel)
		{
			++CurrentLevel;
		}
	}
}
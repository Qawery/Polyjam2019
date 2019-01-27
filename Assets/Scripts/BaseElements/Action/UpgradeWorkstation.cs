using UnityEngine.Assertions;
using System.Collections.Generic;


public class UpgradeWorkstation : Action
{
	private Workstation workstation;


	public override string Title 
	{ 
		get
		{
			return "Upgrade " + workstation.Name + " to level " + (workstation.CurrentLevel + 1).ToString();
		}
	}

	public override Dictionary<Resource, int> ActionCost 
	{
		get
		{
			return workstation.NextUpgradeCost;
		}
	}

	public override void Execute()
	{
		workstation.UpgradeWorkstation();
	}

	public UpgradeWorkstation(Workstation _workstation)
	{
		Assert.IsNotNull(_workstation, "Null workstation");
		Assert.IsTrue(_workstation.MaxLevel > _workstation.CurrentLevel, "Workstation already at max level");
		workstation = _workstation;
	}
}

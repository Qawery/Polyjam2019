using UnityEngine.Assertions;
using System.Collections.Generic;


public class BuildAltar : Action
{
	private Altar altar;


	public override string Name 
	{ 
		get
		{
			return "Build Altar";
		}
	}

	public override Dictionary<Resource, int> ActionCost 
	{
		get
		{
			return altar.NextUpgradeCost;
		}
	}


	public BuildAltar(Altar _altar)
	{
		Assert.IsNotNull(_altar, "Null altar");
		altar = _altar;
	}
}

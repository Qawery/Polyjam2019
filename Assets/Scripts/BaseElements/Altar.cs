using System.Collections.Generic;


public class Altar : Workstation
{
	public override int MaxLevel { get { return 5; } }
	public override WorkstationType Id { get { return WorkstationType.ALTAR; } }
	public override string Name { get { return "Altar"; } }


	public override List<KeyValuePair<Resource, int>> NextUpgradeCost()
	{
		if (CurrentLevel >= MaxLevel)
		{
			return null;
		}
		List<KeyValuePair<Resource, int>> result = new List<KeyValuePair<Resource, int>>();
		switch (CurrentLevel)
		{
			//TODO:
		}
		return result;
	}
}

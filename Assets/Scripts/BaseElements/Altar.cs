using System.Collections.Generic;


public class Altar : Workstation
{
	public override int MaxLevel { get { return 5; } }
	public override WorkstationType Id { get { return WorkstationType.ALTAR; } }
	public override string Name { get { return "Altar"; } }
	public override Dictionary<Resource, int> NextUpgradeCost 
	{ 
		get 
		{
			if (CurrentLevel >= MaxLevel)
			{
				return null;
			}
			Dictionary<Resource, int> result = new Dictionary<Resource, int>();
			result.Add(Resource.FOOD, 2);
			return result;
		}
	}
}

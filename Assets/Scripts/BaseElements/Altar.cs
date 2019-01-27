using System.Collections.Generic;


public class Altar : Workstation
{
	public override int MaxLevel { get { return 5; } }
	public override WorkstationType Id { get { return WorkstationType.Altar; } }
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
			switch (CurrentLevel)
			{
				case 1:
					result.Add(Resource.Clues, 2);
				break;

				case 2:
					result.Add(Resource.Clues, 3);
				break;

				case 3:
					result.Add(Resource.Scrap, 2);
					result.Add(Resource.Clues, 4);
				break;

				case 4:
					result.Add(Resource.Clues, 5);
				break;
			}
			return result;
		}
	}
}

using System.Collections.Generic;


public class Workshop : Workstation
{
	public override int MaxLevel { get { return 3; } }
	public override WorkstationType Id { get { return WorkstationType.Workshop; } }
	public override string Name { get { return "Workshop"; } }
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
					result.Add(Resource.Scrap, 1);
					break;

				case 2:
					result.Add(Resource.Scrap, 2);
					break;

				case 3:
					result.Add(Resource.Scrap, 1);
					result.Add(Resource.Clues, 1);
					break;
			}
			return result;
		}
	}
}


﻿using System.Collections.Generic;


public class AlchemistTable : Workstation
{
	public override int MaxLevel { get { return 5; } }
	public override WorkstationType Id { get { return WorkstationType.Alchemist_Table; } }
	public override string Name { get { return "Alchemist Table"; } }
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
					result.Add(Resource.Food, 2);
					break;

				case 3:
					result.Add(Resource.Herbs, 2);
					break;

				case 4:
					result.Add(Resource.Food, 1);
					result.Add(Resource.Herbs, 1);
					result.Add(Resource.Clues, 1);
					break;
			}
			return result;
		}
	}
}

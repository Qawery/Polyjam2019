using UnityEngine;
using System.Collections.Generic;


public class Explore : Action
{
	public override string Title { get { return "Explore"; } }
	public override Dictionary<Resource, int> ActionCost { get { return new Dictionary<Resource, int>(); } }

	public override void Execute()
	{
		BaseState.Instance.wentToMission = true;
		Application.LoadLevel("Mission01");
	}
}

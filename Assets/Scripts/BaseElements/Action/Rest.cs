using System.Collections.Generic;


public class Rest : Action
{
	public override string Title { get { return "Rest"; } }
	public override Dictionary<Resource, int> ActionCost { get { return new Dictionary<Resource, int>(); } }

	public override void Execute()
	{
	}
}

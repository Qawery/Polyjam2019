using System.Collections.Generic;


public class NoneAction : Action
{
	public override string Title { get { return "None"; } }
	public override Dictionary<Resource, int> ActionCost { get { return new Dictionary<Resource, int>(); } }

	public override void Execute()
	{
	}
}

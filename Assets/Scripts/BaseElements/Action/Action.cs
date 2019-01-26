using System.Collections.Generic;


public abstract class Action
{
	public abstract string Title { get; }
	public abstract Dictionary<Resource, int> ActionCost { get; }
}

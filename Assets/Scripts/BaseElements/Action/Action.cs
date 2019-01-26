using System.Collections.Generic;


public abstract class Action
{
	public abstract string Name { get; }
	public abstract Dictionary<Resource, int> ActionCost { get; }
}

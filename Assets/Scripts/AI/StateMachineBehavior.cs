using UnityEngine;

public abstract class StateMachineBehavior
{
	public virtual void Initialize(AICharacterController owner)
	{
	}
	
	public abstract StateMachineBehavior Update(AICharacterController owner, float deltaTime);

	public virtual void Finish(AICharacterController owner)
	{
	}
}

using UnityEngine.Assertions;

public class StateMachine
{
	private AICharacterController owner;
	private StateMachineBehavior currentBehavior = null;

	public StateMachineBehavior CurrentBehavior
	{
		private get => currentBehavior;
		set
		{
			if (value != currentBehavior)
			{
				currentBehavior?.Finish(owner);
				currentBehavior = value;
				currentBehavior?.Initialize(owner);
			}
		}
	}

	public StateMachine(AICharacterController owner, StateMachineBehavior initBehavior)
	{
		this.owner = owner;
		CurrentBehavior = initBehavior;
	}

	public void Update(float deltaTime)
	{
		CurrentBehavior = CurrentBehavior?.Update(owner, deltaTime);
	}
}

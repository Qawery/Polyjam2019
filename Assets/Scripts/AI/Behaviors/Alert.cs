using Pathfinding;
using UnityEngine;

public class Alert : StateMachineBehavior
{
	private Vector2 soundPosition;
	private float waitTimer = 0;

	public Alert(Vector2 soundPosition)
	{
		this.soundPosition = soundPosition;
	}
	
	public override void Initialize(AICharacterController owner)
	{
		base.Initialize(owner);
		owner.Hearing.OnSoundReceived += OnSoundReceived;
	}

	private void OnSoundReceived(Vector2 position)
	{
		soundPosition = position;
	}

	public override StateMachineBehavior Update(AICharacterController owner, float deltaTime)
	{
		if (owner.Vision.SpottedObject)
		{
			return new Chase();
		}

		if (Vector2.Distance(soundPosition, owner.transform.position.XY()) < 1.0f)
		{
			waitTimer += deltaTime;
			if (waitTimer > 3.0f)
			{
				return new Patrol();
			}
		}
		else
		{
			var aiMovement = owner.GetComponent<IAstarAI>();
			aiMovement.maxSpeed = 0.5f * owner.MaxMovementSpeed;
			aiMovement.destination = soundPosition;
		}
		
		return this;
	}

	public override void Finish(AICharacterController owner)
	{
		base.Finish(owner);
		owner.Hearing.OnSoundReceived -= OnSoundReceived;
	}
}

using Pathfinding;
using UnityEngine.Assertions;
using UnityEngine;

public class Patrol : StateMachineBehavior
{
	int currentPatrolPointIndex = 0;
	private float waitTimer = 0;
		
	public override void Initialize(AICharacterController owner)
	{
		base.Initialize(owner);
		Assert.IsTrue(owner.PatrolPoints.Length > 0);
		float minDist = Vector3.Distance(owner.PatrolPoints[0].Position, owner.transform.position);
		for (int pointIndex = 1; pointIndex < owner.PatrolPoints.Length; ++pointIndex)
		{
			float dist = Vector3.Distance(owner.PatrolPoints[pointIndex].Position, owner.transform.position);
			if (dist < minDist)
			{
				minDist = dist;
				currentPatrolPointIndex = pointIndex;
			}
		}
	}

	public override StateMachineBehavior Update(AICharacterController owner, float deltaTime)
	{
		PatrolPointData currentPatrolPoint = owner.PatrolPoints[currentPatrolPointIndex];
		Vector3 dest = currentPatrolPoint.Position;
		if (Vector3.Distance(dest, owner.transform.position) < 1.0f)
		{
			waitTimer += deltaTime;
			if (waitTimer > currentPatrolPoint.waitTime)
			{
				++currentPatrolPointIndex;
				if (currentPatrolPointIndex == owner.PatrolPoints.Length)
				{
					currentPatrolPointIndex = 0;
				}

				waitTimer = 0;
			}
		}
		else
		{
			var aiMovement = owner.GetComponent<IAstarAI>();
			aiMovement.maxSpeed = 0.25f * owner.MaxMovementSpeed;
			aiMovement.destination = currentPatrolPoint.Position;
		}
		
		//TODO: check vision and hearing and transition to alerted or chase state if necessary
		return this;
	}
}

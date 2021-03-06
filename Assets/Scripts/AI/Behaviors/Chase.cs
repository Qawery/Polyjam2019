﻿using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Chase : StateMachineBehavior
{
	private Vector2 lastKnownPosition;
	
	public override StateMachineBehavior Update(AICharacterController owner, float deltaTime)
	{
		var aiMovement = owner.GetComponent<IAstarAI>();
		if (owner.Vision.SpottedObject)
		{
			lastKnownPosition = owner.Vision.SpottedObject.transform.position.XY();
			var combat = owner.GetComponent<CombatComponent>();
			if (Vector3.Distance(lastKnownPosition, owner.transform.position) <
			    0.8f * (combat.MeleeRange + owner.Vision.SpottedObject.GetComponent<CircleCollider2D>().radius))
			{
				aiMovement.destination = owner.transform.position;
				Vector2 direction = lastKnownPosition - owner.transform.position.XY();
				float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
				var targetRot = Quaternion.Euler(0, 0, targetAngle);
				owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, targetRot, 360.0f * deltaTime);
				combat.PerformMeleeAttack();
			}
			else
			{
				aiMovement.maxSpeed = owner.MaxMovementSpeed;
				aiMovement.destination = lastKnownPosition;
			}
		}
		else
		{
			if (Vector3.Distance(lastKnownPosition, owner.transform.position) < 1.0f)
			{
				return new Alert(lastKnownPosition);
			}
		}

		return this;
	}
}

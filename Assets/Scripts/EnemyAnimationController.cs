using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
	[SerializeField] private float animationSpeedMultiplier = 0.1f;
	[SerializeField] private float minAnimationSpeed = 0.0f;
	[SerializeField] private float maxAnimationSpeed = 2.0f;
	
	[SerializeField] private string walkSpeedKey = "speed";
	[SerializeField] private string meleeAttackTriggerKey = "meleeTrigger";
	
	private IAstarAI aiMovement = null;
	private Animator animator = null;
	
	private int walkSpeedHash;
	private int meleeAttackTriggerHash;


	private void Awake()
	{
		aiMovement = GetComponent<IAstarAI>();
		animator = GetComponent<Animator>();
		walkSpeedHash = Animator.StringToHash(walkSpeedKey);
		meleeAttackTriggerHash = Animator.StringToHash(meleeAttackTriggerKey);
		
		var combat = GetComponent<CombatComponent>();
		combat.OnMeleeAttack += OnMeleeAttack;
	}

	private void OnMeleeAttack()
	{
		animator.SetTrigger(meleeAttackTriggerHash);
	}
	
	void Update()
	{
		animator.SetFloat(walkSpeedHash, Mathf.Clamp(aiMovement.velocity.magnitude * animationSpeedMultiplier, minAnimationSpeed, maxAnimationSpeed));
	}
}

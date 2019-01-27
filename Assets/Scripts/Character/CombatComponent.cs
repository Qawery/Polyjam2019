using UnityEngine;

public class CombatComponent : MonoBehaviour
{
	[SerializeField] private LayerMask enemyLayers;
	[SerializeField] private float shotRange = 10.0f;
	[SerializeField] private int shotDamage = 30;
	[SerializeField] private float meleeRange = 0.5f;
	[SerializeField] private int meleeDamage = 100;
	[SerializeField] private float attackCooldown = 1.0f;
	[SerializeField] private float shotSoundRange = 30.0f;

	private float cooldownRemaining;

	public float MeleeRange => meleeRange;

	public event System.Action OnMeleeAttack;
	public event System.Action OnShot;

	public void Shoot()
	{
		if (cooldownRemaining <= 0)
		{
			PerformAttack(shotRange, shotDamage);
			GetComponent<SoundEmitter>().EmitSound(shotSoundRange);
			OnShot?.Invoke();
		}
	}
	
	public void PerformMeleeAttack()
	{
		if (cooldownRemaining <= 0)
		{
			PerformAttack(meleeRange, meleeDamage);
			OnMeleeAttack?.Invoke();
		}
	}

	private void PerformAttack(float distance, int damage)
	{
		var hit = Physics2D.Raycast(transform.position, transform.up, distance, enemyLayers);
		if (hit.collider != null)
		{
			var health = hit.collider.GetComponent<HealthComponent>();
			if (health != null)
			{
				health.ReceiveDamage(damage);
			}
		}

		cooldownRemaining = attackCooldown;
	}

	private void Update()
	{
		cooldownRemaining -= Time.deltaTime;
	}
}

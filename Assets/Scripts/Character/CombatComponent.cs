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
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip audioClip;
	private float cooldownRemaining;
	public event System.Action OnMeleeAttack;
	public event System.Action OnShot;


	public float MeleeRange => meleeRange;


	private void Awake()
	{
		if (gameObject.tag == "Player" && BaseState.Instance != null)
		{
			int currentLevel = BaseState.Instance.GetWorkstationOfType(WorkstationType.Workshop).CurrentLevel;
			int maxLevel = BaseState.Instance.GetWorkstationOfType(WorkstationType.Workshop).MaxLevel;
			meleeRange += (meleeRange * (currentLevel / maxLevel));
			shotRange += (shotRange * (currentLevel / maxLevel));
			meleeDamage += (meleeDamage * (currentLevel / maxLevel));
			shotDamage += (shotDamage * (currentLevel / maxLevel));
		}
	}

	private void Update()
	{
		cooldownRemaining -= Time.deltaTime;
	}

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
			audioSource.PlayOneShot(audioClip);
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
}

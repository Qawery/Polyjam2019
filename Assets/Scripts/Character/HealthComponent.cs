using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	[SerializeField] private int maxValue;
	private int currentValue;

	public int CurrentValue
	{
		get => currentValue;

		private set
		{
			currentValue = value;
			OnValueChanged?.Invoke(currentValue);
			if (currentValue == 0)
			{
				OnDeath?.Invoke();
			}
		}
	}
	
	public event System.Action OnDeath;
	public event System.Action<int> OnValueChanged;
	public event System.Action<int> OnDamageReceived;

	public void Reset()
	{
		CurrentValue = maxValue;
	}

	public void ReceiveDamage(int damage)
	{
		CurrentValue -= damage;
		OnDamageReceived?.Invoke(damage);
	}

	private void Awake()
	{
		Reset();
	}
}

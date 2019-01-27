using UnityEngine;


public class HealthComponent : MonoBehaviour
{
	[SerializeField] private int maxValue;
	private int currentValue;
	public event System.Action OnDeath;
	public event System.Action<int> OnValueChanged;
	public event System.Action<int> OnDamageReceived;


	public float HealthLeftNormalized { get { return (float)CurrentValue / (maxValue > 0 ? maxValue : 100); } }

	public int CurrentValue
	{
		get => currentValue;

		private set
		{
			currentValue = value;
			OnValueChanged?.Invoke(currentValue);
			if (currentValue <= 0)
			{
				OnDeath?.Invoke();
				Debug.Log(name + " died");
				Destroy(gameObject); //TODO: remove this after implementing proper death logic
			}
		}
	}


	private void Awake()
	{
		if (gameObject.tag == "Player" && BaseState.Instance != null)
		{
			int currentLevel = BaseState.Instance.GetWorkstationOfType(WorkstationType.Alchemist_Table).CurrentLevel;
			int maxLevel = BaseState.Instance.GetWorkstationOfType(WorkstationType.Alchemist_Table).MaxLevel;
			maxValue += (maxValue * (currentLevel / maxLevel));
		}
		RestoreValue();
	}

	public void RestoreValue()
	{
		CurrentValue = maxValue;
	}

	public void ReceiveDamage(int damage)
	{
		Debug.Log(name + " received " + damage + " damage.");
		CurrentValue -= damage;
		OnDamageReceived?.Invoke(damage);
	}
}

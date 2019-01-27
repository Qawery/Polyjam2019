using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections.Generic;
using static Polyjam2019.Pickables.BasePickableScriptableObject;


public class MissionFlowManager : MonoBehaviour
{
	[SerializeField] private Text endGameTitle;
	[SerializeField] private Text endGameMessage;
	[SerializeField] private Button endGameButton;
	private FadeOutScreen fadeOutScreen;


	private void Awake()
	{
		Assert.IsNotNull(endGameTitle, "Missing endGameTitle");
		Assert.IsNotNull(endGameMessage, "Missing endGameMessage");
		Assert.IsNotNull(endGameButton, "Missing endGameButton");
		EndGameControlsSetActive(false);
		fadeOutScreen = Resources.FindObjectsOfTypeAll<FadeOutScreen>()[0];
		Assert.IsNotNull(fadeOutScreen, "Missing dayEndAnimation");
	}

	private void Start()
	{
		fadeOutScreen.gameObject.SetActive(true);
		fadeOutScreen.SetDark();
		fadeOutScreen.OnLighteningEnd += BeginMission;
		fadeOutScreen.StartLightening();
		HealthComponent playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
		playerHealth.OnDeath += PlayerDeath;
	}

	private void BeginMission()
	{
		fadeOutScreen.OnLighteningEnd -= BeginMission;
		fadeOutScreen.gameObject.SetActive(false);
	}

	public void GoHomeStart()
	{
		fadeOutScreen.gameObject.SetActive(true);
		fadeOutScreen.OnDarkeningEnd += GoHomeEnd;
		fadeOutScreen.StartDarkening();
	}

	private void GoHomeEnd()
	{
		fadeOutScreen.OnDarkeningEnd -= GoHomeEnd;
		if (BaseState.Instance != null)
		{
			Polyjam2019.CharacterEquipment equipment = FindObjectOfType<Polyjam2019.CharacterEquipment>();
			Dictionary<Resource, int> resourcesGathered = new Dictionary<Resource, int>();
			foreach (PickableData data in equipment.GetItemsIterator())
			{
				if (!resourcesGathered.ContainsKey(data.Resource))
				{
					resourcesGathered.Add(data.Resource, 0);
				}
				resourcesGathered[data.Resource]++;
			}
			BaseState.Instance.ChangeValuesOfResources(resourcesGathered);
		}
		Application.LoadLevel("Base");
	}

	public void PlayerDeath()
	{
		fadeOutScreen.gameObject.SetActive(true);
		fadeOutScreen.OnDarkeningEnd += DefeatScreen;
		fadeOutScreen.StartDarkening();
	}

	private void DefeatScreen()
	{
		fadeOutScreen.OnDarkeningEnd -= DefeatScreen;
		EndGameControlsSetActive(true);
	}

	private void EndGameControlsSetActive(bool newState)
	{
		endGameTitle.gameObject.SetActive(newState);
		endGameMessage.gameObject.SetActive(newState);
		endGameButton.gameObject.SetActive(newState);
	}
}
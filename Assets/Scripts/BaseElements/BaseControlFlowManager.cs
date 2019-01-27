using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class BaseControlFlowManager : MonoBehaviour
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
		fadeOutScreen.SetDark();
		fadeOutScreen.gameObject.SetActive(true);
		if (BaseState.Instance.wentToMission)
		{
			BaseState.Instance.wentToMission = false;
			fadeOutScreen.OnLighteningEnd += BeginNextDay;
		}
		else
		{
			fadeOutScreen.OnLighteningEnd += FirstDay;
		}
		GameEndConditionsCheck();
	}

	private void FirstDay()
	{
		fadeOutScreen.OnLighteningEnd -= FirstDay;
		fadeOutScreen.gameObject.SetActive(false);
	}

	public void BeginNextDay()
	{
		fadeOutScreen.OnLighteningEnd -= BeginNextDay;
		--BaseState.Instance.DaysLeft;
		if (BaseState.Instance.ThreatLevel > 0)
		{
			--BaseState.Instance.ThreatLevel;
		}
		BaseState.Instance.ChangeValueOfResource(Resource.Food, -1);
		fadeOutScreen.gameObject.SetActive(false);
	}

	public void BeginDayEnd()
	{
		fadeOutScreen.gameObject.SetActive(true);
		fadeOutScreen.OnDarkeningEnd += BeginActionResolve;
		fadeOutScreen.StartDarkening();
	}

	private void BeginActionResolve()
	{
		fadeOutScreen.OnDarkeningEnd -= BeginActionResolve;
		Action executedAction = BaseState.Instance.SelectedAction;
		BaseState.Instance.SelectedAction = new NoneAction();
		BaseState.Instance.ChangeValuesOfResources(executedAction.ActionCost, true);
		executedAction.Execute();
		if (!BaseState.Instance.wentToMission)
		{
			fadeOutScreen.OnLighteningEnd += BeginNextDay;
			GameEndConditionsCheck();
		}
	}

	public void GameEndConditionsCheck()
	{
		bool gameEnded = false;
		if (BaseState.Instance.DaysLeft <= 0)
		{
			endGameTitle.text = "Defeat";
			endGameMessage.text = "Your failed to perform ritual in time and world has been overrun by monsters";
			gameEnded = true;
		}
		else if (BaseState.Instance.GetResourceQuantity(Resource.Food) <= 0)
		{
			endGameTitle.text = "Defeat";
			endGameMessage.text = "Your starved to death";
			gameEnded = true;
		}
		else if (BaseState.Instance.ThreatLevel > 10)
		{
			endGameTitle.text = "Defeat";
			endGameMessage.text = "You caused too much noise and monsters found your home";
			gameEnded = true;
		}
		else if (BaseState.Instance.GetWorkstationOfType(WorkstationType.Altar) != null && BaseState.Instance.GetWorkstationOfType(WorkstationType.Altar).CurrentLevel == BaseState.Instance.GetWorkstationOfType(WorkstationType.Altar).MaxLevel)
		{
			endGameTitle.text = "Victory";
			endGameMessage.text = "You succesfully performed ritual and sealed the gate to monster world";
			gameEnded = true;
		}
		if (gameEnded)
		{
			EndGameControlsSetActive(true);
		}
		else
		{
			fadeOutScreen.StartLightening();
		}
	}

	private void EndGameControlsSetActive(bool newState)
	{
		endGameTitle.gameObject.SetActive(newState);
		endGameMessage.gameObject.SetActive(newState);
		endGameButton.gameObject.SetActive(newState);
	}
}

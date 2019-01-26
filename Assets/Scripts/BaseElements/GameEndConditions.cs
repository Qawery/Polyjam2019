using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class GameEndConditions : MonoBehaviour
{
	[SerializeField] private Text endGameMessage;
	[SerializeField] private Button endGameButton;
	private FadeOutScreen dayEndAnimation;
	private BaseManager baseManager;


	private void Awake()
	{
		Assert.IsNotNull(endGameMessage, "Missing endGameMessage");
		endGameMessage.gameObject.SetActive(false);
		Assert.IsNotNull(endGameButton, "Missing endGameButton");
		endGameButton.gameObject.SetActive(false);
		dayEndAnimation = Resources.FindObjectsOfTypeAll<FadeOutScreen>()[0];
		Assert.IsNotNull(dayEndAnimation, "Missing dayEndAnimation");
		baseManager = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseManager, "Missing baseManager");
		baseManager.OnPostActionResolve += GameEndConditionsCheck;
	}

	public void GameEndConditionsCheck()
	{
		bool gameEnded = false;
		if (baseManager.DaysLeft <= 0)
		{
			endGameMessage.text = "Defeat\n\n";
			endGameMessage.text += "Your failed to perform ritual in time and world has been overrun by monsters";
			gameEnded = true;
		}
		else if (baseManager.GetResourceQuantity(Resource.Food) <= 0)
		{
			endGameMessage.text = "Defeat\n\n";
			endGameMessage.text += "Your starved to death";
			gameEnded = true;
		}
		else if (baseManager.ThreatLevel > 10)
		{
			endGameMessage.text = "Defeat\n\n";
			endGameMessage.text += "You caused too much noise and monsters found your home";
			gameEnded = true;
		}
		else if (baseManager.GetWorkstationOfType(WorkstationType.Altar) != null && baseManager.GetWorkstationOfType(WorkstationType.Altar).CurrentLevel == baseManager.GetWorkstationOfType(WorkstationType.Altar).MaxLevel)
		{
			endGameMessage.text = "Victory\n\n";
			endGameMessage.text += "You succesfully performed ritual and sealed the gate to monster world";
			gameEnded = true;
		}
		if (gameEnded)
		{
			endGameMessage.gameObject.SetActive(true);
			endGameButton.gameObject.SetActive(true);
		}
		else
		{
			dayEndAnimation.StartLightening();
		}
	}
}

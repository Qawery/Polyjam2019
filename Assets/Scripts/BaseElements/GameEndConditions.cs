using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class GameEndConditions : MonoBehaviour
{
	[SerializeField] private Text endGameMessage;
	[SerializeField] private Button endGameButton;
	private DayEndAnimation dayEndAnimation;
	private BaseManager baseManager;


	private void Awake()
	{
		Assert.IsNotNull(endGameMessage, "Missing endGameMessage");
		endGameMessage.gameObject.SetActive(false);
		Assert.IsNotNull(endGameButton, "Missing endGameButton");
		endGameButton.gameObject.SetActive(false);
		dayEndAnimation = Resources.FindObjectsOfTypeAll<DayEndAnimation>()[0];
		Assert.IsNotNull(dayEndAnimation, "Missing dayEndAnimation");
		baseManager = FindObjectOfType<BaseManager>();
		Assert.IsNotNull(baseManager, "Missing baseManager");
		baseManager.OnPostActionResolve += GameEndConditionsCheck;
	}

	public void GameEndConditionsCheck()
	{
		if (baseManager.DaysLeft <= 0 || baseManager.GetResourceQuantity(Resource.FOOD) <= 0)
		{
			endGameMessage.text = "Defeat";
			endGameMessage.gameObject.SetActive(true);
			endGameButton.gameObject.SetActive(true);
		}
		else if (baseManager.GetWorkstationOfType(WorkstationType.ALTAR) != null && baseManager.GetWorkstationOfType(WorkstationType.ALTAR).CurrentLevel == baseManager.GetWorkstationOfType(WorkstationType.ALTAR).MaxLevel)
		{
			endGameMessage.text = "Victory";
			endGameMessage.gameObject.SetActive(true);
			endGameButton.gameObject.SetActive(true);
		}
		else
		{
			dayEndAnimation.StartLightening();
		}
	}
}

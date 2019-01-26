using UnityEngine;


public class ActionButtonsManager : MonoBehaviour
{
	[SerializeField] private SelectActionButton altarButtonTest;


	private void Start()
	{
		altarButtonTest.SetAction(new BuildAltar(FindObjectOfType<BaseState>().GetWorkstationOfType(WorkstationType.ALTAR) as Altar));
	}
}

using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class MissionExitTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name.Equals("PlayerCharacter"))
		{
			FindObjectOfType<MissionFlowManager>().GoHomeStart();
		}
	}
}
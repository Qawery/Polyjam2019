using Polyjam2019;
using UnityEngine;


[RequireComponent(typeof(CharacterMovement))]
public class PlayerCharacterController : MonoBehaviour
{
	[SerializeField] private float minDistanceToCursor = 0.1f;
	[SerializeField] private float maxDistanceToCursor = 2f;
	[SerializeField] private float distanceFalloffExponent = 1.0f;	
	private CharacterMovement movement;
    private CharacterEquipment equipment;
	
	private void Awake()
	{
		movement = GetComponent<CharacterMovement>();
		distanceFalloffExponent = Mathf.Max(0, distanceFalloffExponent);

        equipment = GetComponent<CharacterEquipment>();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 toCursor = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).XY();
			float cursorDistance = toCursor.magnitude;
			float normalizedDistance = Mathf.Clamp01((cursorDistance - minDistanceToCursor) / (maxDistanceToCursor - minDistanceToCursor));
			float speedPercentage = Mathf.Pow(normalizedDistance, distanceFalloffExponent);
			movement.Move(toCursor.normalized, speedPercentage);
		}
		else
		{
			movement.Stop();
		}

        if(Input.GetKeyDown(KeyCode.E) && equipment != null && equipment.ItemAvailableForPickup)
        {
            equipment.PickItem();
        }
	}
}

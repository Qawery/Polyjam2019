using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private float velocityChangeSpeed = 1.0f; //TODO: add Range attribute
	[SerializeField] private float maxSpeed = 2.0f;
	
	private new Rigidbody2D rigidbody2D;
	private Vector2 targetVelocity;
	
	/// <summary>
	/// Sets target direction and speed.
	/// </summary>
	/// <param name="direction">Desired movement direction</param>
	/// <param name="speedPercentage">A value from [0,1] range, determining target speed</param>
	public void Move(Vector2 direction, float speedPercentage)
	{
		speedPercentage = Mathf.Clamp01(speedPercentage);
		targetVelocity = direction.normalized * speedPercentage * maxSpeed;
	}
	
	/// <summary>
	/// Stops movement gradually
	/// </summary>
	public void Stop()
	{
		targetVelocity = Vector2.zero;
	}

	/// <summary>
	/// Stops movement immediately, disregarding inertia
	/// </summary>
	public void StopImmediate()
	{
		targetVelocity = rigidbody2D.velocity = Vector2.zero;
	}
	
	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private const float minSpeedToRotate = 0.001f;
	private void FixedUpdate()
	{
		rigidbody2D.velocity = Vector2.MoveTowards(rigidbody2D.velocity, targetVelocity, velocityChangeSpeed * Time.fixedDeltaTime);
		if (rigidbody2D.velocity.magnitude > minSpeedToRotate)
		{
			Vector2 movementDir = rigidbody2D.velocity.normalized;
			rigidbody2D.rotation = Mathf.Atan2(movementDir.y, movementDir.x) * Mathf.Rad2Deg - 90;
		}
	}
}

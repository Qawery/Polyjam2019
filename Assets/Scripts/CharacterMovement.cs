using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private float rotationSpeedDegrees = 360.0f;
	[SerializeField] private float velocityChangeSpeed = 1.0f; //TODO: add Range attribute
	[SerializeField] private float maxSpeed = 2.0f;

	[SerializeField] private float maxRotDifToStartMovement = 5.0f;
	
	private new Rigidbody2D rigidbody2D;
	public Vector2 TargetVelocity { get; private set; }
	
	/// <summary>
	/// Sets target direction and speed.
	/// </summary>
	/// <param name="direction">Desired movement direction</param>
	/// <param name="speedPercentage">A value from [0,1] range, determining target speed</param>
	public void Move(Vector2 direction, float speedPercentage)
	{
		speedPercentage = Mathf.Clamp01(speedPercentage);
		TargetVelocity = direction.normalized * speedPercentage * maxSpeed;
	}
	
	/// <summary>
	/// Stops movement gradually
	/// </summary>
	public void Stop()
	{
		TargetVelocity = Vector2.zero;
	}

	/// <summary>
	/// Stops movement immediately, disregarding inertia
	/// </summary>
	public void StopImmediate()
	{
		TargetVelocity = rigidbody2D.velocity = Vector2.zero;
	}
	
	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private const float minSpeedToMove = 0.01f;
	private void FixedUpdate()
	{
		rigidbody2D.angularVelocity = 0;
		
		Vector2 movementDir = TargetVelocity.normalized;
		float targetAngle = Mathf.Atan2(movementDir.y, movementDir.x) * Mathf.Rad2Deg - 90;
		var targetRot = Quaternion.Euler(0, 0, targetAngle);
		
		if (TargetVelocity.magnitude > minSpeedToMove)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeedDegrees * Time.fixedDeltaTime);
		}
		
		Vector2 finalVelocity = Quaternion.Angle(targetRot, transform.rotation) < maxRotDifToStartMovement ? TargetVelocity : Vector2.zero;
		rigidbody2D.velocity = Vector2.MoveTowards(rigidbody2D.velocity, finalVelocity, velocityChangeSpeed * Time.fixedDeltaTime);
	}
}


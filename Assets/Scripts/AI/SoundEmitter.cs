using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundReceiver
{
	void ReceiveSound(Vector2 position);
}

public class SoundEmitter : MonoBehaviour
{
	[SerializeField] private SpriteRenderer soundCircle;
	[SerializeField] private float alphaTransitionSpeed = 5.0f;
	[SerializeField] private float maxAlpha = 0.3f;

	private float targetAlpha;
	
	public void EmitSound(float radius)
	{
		var colliders = Physics2D.OverlapCircleAll(transform.position.XY(), radius);
		foreach (var collider in colliders)
		{
			var receiver = collider.GetComponent<ISoundReceiver>();
			receiver?.ReceiveSound(transform.position.XY());
		}

		soundCircle.transform.localScale = Vector3.one * radius;
		targetAlpha = maxAlpha;
	}

	void Update()
	{
		Color currentColor = soundCircle.color;
		if (Mathf.Abs(currentColor.a - targetAlpha) < 0.001f)
		{
			targetAlpha = 0;
		}

		currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, alphaTransitionSpeed * Time.deltaTime);
		soundCircle.color = currentColor;
	}
}


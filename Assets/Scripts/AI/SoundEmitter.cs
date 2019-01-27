using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundReceiver
{
	void ReceiveSound(Vector2 position);
}

public static class SoundEmitter
{
	public static void EmitSound(Vector2 position, float radius)
	{
		var colliders = Physics2D.OverlapCircleAll(position, radius);
		foreach (var collider in colliders)
		{
			var receiver = collider.GetComponent<ISoundReceiver>();
			receiver?.ReceiveSound(position);
		}
	}
}


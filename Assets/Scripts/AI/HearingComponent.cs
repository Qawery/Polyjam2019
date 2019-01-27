using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingComponent : MonoBehaviour, ISoundReceiver
{
	public event System.Action<Vector2> OnSoundReceived;
	public void ReceiveSound(Vector2 position)
	{
		Debug.Log(name + " received sound at " + position);
		OnSoundReceived?.Invoke(position);
	}
}

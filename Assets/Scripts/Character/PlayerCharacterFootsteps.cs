using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterFootsteps : MonoBehaviour
{
	[SerializeField] private float maxSoundRadius = 10.0f;
	[SerializeField] private float speedForMaxSoundRadius = 4.0f;
	[SerializeField] private float stepDistance = 1.0f;

	[SerializeField] private AudioSource footstepSource;
	
	private new Rigidbody2D rigidbody2D;
	private SoundEmitter soundEmitter;
	private Vector2 previousFootstepPos;

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		soundEmitter = GetComponent<SoundEmitter>();
		previousFootstepPos = rigidbody2D.position;
	}

	void Update()
	{
		float distFromPrev = Vector2.Distance(previousFootstepPos, rigidbody2D.position);

		if (distFromPrev > stepDistance)
		{
			previousFootstepPos = rigidbody2D.position;
			float speed = rigidbody2D.velocity.magnitude;
			float radius = maxSoundRadius * Mathf.Clamp01(speed / speedForMaxSoundRadius);
			soundEmitter.EmitSound(radius);
		}

		if (rigidbody2D.velocity.magnitude > 0.1f)
		{
			if (!footstepSource.isPlaying)
			{
				footstepSource.Play();
			}
		}
		else if (footstepSource.isPlaying)
		{
			footstepSource.Stop();
		}
	}
}

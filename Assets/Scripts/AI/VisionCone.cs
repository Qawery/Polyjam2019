using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
	[SerializeField] private LayerMask raycastMask;

	private GameObject objectInCollider = null;
	public GameObject SpottedObject { get; private set; }

	private void OnTriggerEnter2D(Collider2D other)
	{
		objectInCollider = other.gameObject;
		Debug.Log("Entering cone");

	}

	private void OnTriggerExit2D(Collider2D other)
	{
		objectInCollider = null;
		Debug.Log("Leaving cone");
	}

	private void Update()
	{
		SpottedObject = null;
		if (objectInCollider != null)
		{
			var hit = Physics2D.Raycast(transform.position, objectInCollider.transform.position - transform.position, Mathf.Infinity, raycastMask);
			if (hit.collider != null && hit.collider.gameObject == objectInCollider)
			{
				SpottedObject = objectInCollider;
			}
		}
	}
}

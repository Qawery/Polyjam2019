using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
	[SerializeField] private LayerMask raycastMask;
	
	private HashSet<GameObject> spottedObjects = new HashSet<GameObject>();
	public IEnumerable<GameObject> SpottedEnemies => spottedObjects;

	private void OnTriggerEnter2D(Collider2D other)
	{
		var hit = Physics2D.Raycast(transform.position, other.transform.position,
			Vector3.Distance(transform.position, other.transform.position), raycastMask);
		if (hit.collider == other)
		{
			spottedObjects.Add(other.gameObject);
		}
	}
}

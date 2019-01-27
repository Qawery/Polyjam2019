using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class SpawnOnDeath : MonoBehaviour
{
	[SerializeField] private GameObject[] spawnedObjects;
	
	private void Awake()
	{
		GetComponent<HealthComponent>().OnDeath += OnDeath;
	}

	private void OnDeath()
	{
		foreach (var obj in spawnedObjects)
		{
			Instantiate(obj, transform.position, Quaternion.identity);
		}
	}
}

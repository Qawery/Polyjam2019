using UnityEngine;


namespace TT
{
	public class ForceUpwardRotation : MonoBehaviour
	{
		public void LateUpdate()
		{
			Vector3 newPosition = transform.parent.transform.position + Vector3.up * Vector2.Distance(transform.position.XY(), transform.parent.transform.position.XY());
			newPosition.z = transform.position.z;
			transform.position = newPosition;
			transform.rotation = Quaternion.Euler(Vector3.zero);
		}
	}
}

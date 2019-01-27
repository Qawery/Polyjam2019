using UnityEngine;


public class ForceUpwardRotation : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    public void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = target.position - Vector3.forward * 10;
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
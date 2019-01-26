using UnityEngine;


public class ForceUpwardRotation : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    public void LateUpdate()
    {
        Vector3 newPosition = target.position - Vector3.forward * 10;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
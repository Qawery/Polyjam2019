using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private BoxCollider2D levelBoundary;

    private Vector2 min, max;

    private void Awake()
    {
        min = levelBoundary.bounds.min;
        max = levelBoundary.bounds.max;
    }

    public void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = target.position - Vector3.forward * 10;
            Vector2 cameraMin = newPosition.XY() - new Vector2(Camera.main.aspect, 1) * Camera.main.orthographicSize;
            Vector2 cameraMax = newPosition.XY() + new Vector2(Camera.main.aspect, 1) * Camera.main.orthographicSize;

            if (cameraMin.x < min.x)
            {
                newPosition.x += (min.x - cameraMin.x);
            }
            else if(cameraMax.x > max.x)
            {
                newPosition.x += (max.x - cameraMax.x);
            }

            if (cameraMin.y < min.y)
            {
                newPosition.y += (min.y - cameraMin.y);
            }
            else if (cameraMax.y > max.y)
            {
                newPosition.y += (max.y - cameraMax.y);
            }
            
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}

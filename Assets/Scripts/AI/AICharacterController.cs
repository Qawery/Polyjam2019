using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PatrolPointData
{
    [SerializeField] private Transform transform;
    public float waitTime;
    public Vector3 Position => transform.position;
}

public class AICharacterController : MonoBehaviour
{
    [SerializeField] private PatrolPointData[] patrolPoints;
    [SerializeField] private float maxMovementSpeed = 5.0f;
    private StateMachine stateMachine = null;
    
    public VisionCone Vision { get; private set; }
    public HearingComponent Hearing { get; private set; }
    public PatrolPointData[] PatrolPoints => patrolPoints;
    public float MaxMovementSpeed => maxMovementSpeed;
    
    void Awake()
    {
        Vision = GetComponent<VisionCone>();
        Hearing = GetComponent<HearingComponent>();
        stateMachine = new StateMachine(this, new Patrol());
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }
}

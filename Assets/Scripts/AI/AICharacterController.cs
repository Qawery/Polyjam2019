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
    
    public Blackboard SharedBlackboard { get; set; }
    public PatrolPointData[] PatrolPoints => patrolPoints;
    public float MaxMovementSpeed => maxMovementSpeed;

    void Awake()
    {
        stateMachine = new StateMachine(this, new Patrol());
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }
}

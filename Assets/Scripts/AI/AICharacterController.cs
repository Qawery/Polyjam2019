using System.Collections.Generic;
using UnityEngine;

public class AICharacterController : MonoBehaviour
{
    [SerializeField] private GameObject patrolRoute;
    [SerializeField] private float maxMovementSpeed = 5.0f;
    private StateMachine stateMachine = null;
    private PatrolPoint[] patrolPoints;

    
    public VisionCone Vision { get; private set; }
    public HearingComponent Hearing { get; private set; }
    public PatrolPoint[] PatrolPoints => patrolPoints;
    public float MaxMovementSpeed => maxMovementSpeed;
    
    void Awake()
    {
        Vision = GetComponent<VisionCone>();
        Hearing = GetComponent<HearingComponent>();
        patrolPoints = patrolRoute.GetComponentsInChildren<PatrolPoint>();
        stateMachine = new StateMachine(this, new Patrol());
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }
}

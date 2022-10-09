using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum movementStates
{
    walking,
    running
}

public class GroupMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AttackStanceManager attackStanceManager;
    [SerializeField] private PlayerAudio audio;
    private Camera cam;

    [Header("Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runRange;
    [SerializeField] private float runSpeed;
    [SerializeField] private float stoppingDistance;
    private float speed;
    private bool orderIsValid;
    private Vector3 leaderTargetPosition;

    [Header("States")]
    [SerializeField] private movementStates state;
    private UnityEvent onMouseRightClick;

    private void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!attackStanceManager) attackStanceManager = GetComponent<AttackStanceManager>();
        if (!audio) audio = GetComponent<PlayerAudio>();
        cam = Camera.main;
        speed = walkSpeed;
        onMouseRightClick = new UnityEvent();
        leaderTargetPosition = Vector3.zero;
    }

    private void OnEnable()
    {
        onMouseRightClick.AddListener(movementOrder);
    }

    private void OnDisable()
    {
        onMouseRightClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onMouseRightClick.Invoke();
        }

        if (Vector3.Distance(transform.position, leaderTargetPosition) <= stoppingDistance)
        {
            agent.isStopped = true;
            agent.ResetPath();
            audio.SetNoFootsteps();

        }
    }

    private void movementOrder()
    {
        // freeze initial position
        Vector3 positionOnOrdrer = transform.position;
        orderIsValid = false;

        // detect mouse position
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Debug.Log("raycast hit");
            orderIsValid = true;
            
            leaderTargetPosition = hit.point;
            
            // navmesh management
            agent.ResetPath();

            if (!attackStanceManager.IsInAttackStance())
            {
                agent.SetDestination(leaderTargetPosition);
            }
        }

        if (orderIsValid)
        {
            OrderDistanceProcessing(positionOnOrdrer);
        }
    }

    private void OrderDistanceProcessing(Vector3 initialPosition)
    {
        // distance processing
        float distance = Vector3.Distance(initialPosition, leaderTargetPosition);
        if ( distance >= runRange)
        {
            state = movementStates.running;
            speed = runSpeed;
            audio.SetRunAudio();
        } else
        {
            state = movementStates.walking;
            speed = walkSpeed;
            audio.SetWalkAudio();
        }

        agent.speed = speed;
    }

    public Vector3 GetLeaderTargetPosition()
    {
        return leaderTargetPosition;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetWalkSpeed()
    {
        return walkSpeed;
    }
    public float GetRunSpeed()
    {
        return runSpeed;
    }
    public bool OrderIsValid()
    {
        return orderIsValid;
    }

    public UnityEvent getOnMouseRightClickEvent()
    {
        return onMouseRightClick;
    }

    public movementStates GetMovementState()
    {
        return state;
    }
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}

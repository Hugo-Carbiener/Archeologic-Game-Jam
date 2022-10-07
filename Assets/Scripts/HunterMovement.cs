using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class HunterMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GroupMovement groupMovementManager;

    private void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(groupMovementManager);
    }

    private void Start()
    {
        groupMovementManager.getOnMouseRightClickEvent().AddListener(SetTargetPosition);
    }

    private void OnEnable()
    {
       
    }

    private void SetTargetPosition()
    {
        Vector3 targetPosition;
        Vector3 offset = - groupMovementManager.transform.position + transform.position;
        Vector3 groupTargetPosition = groupMovementManager.GetTargetPosition();
        if (groupTargetPosition != null)
        {
            targetPosition = groupTargetPosition + offset;
            agent.SetDestination(targetPosition);
        }
    }
}

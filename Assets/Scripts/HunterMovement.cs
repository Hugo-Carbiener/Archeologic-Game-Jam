using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class HunterMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GroupMovement groupMovementManager;
    [SerializeField] private AttackStanceManager attackStanceManager;

    private void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(groupMovementManager);
        if (!attackStanceManager) attackStanceManager = GetComponent<AttackStanceManager>();
    }

    private void Start()
    {
        groupMovementManager.getOnMouseRightClickEvent().AddListener(SetTargetPosition);
        StartCoroutine(RegroupTimer());
    }

    private IEnumerator RegroupTimer()
    {
        yield return new WaitForSeconds(10f);
        Regroup();
    }

    /**
     *  Function that will handle the regrouping of the player units
     */
    private void Regroup()
    {
        print("REGROUPING");
        agent.ResetPath();

        if (!attackStanceManager.IsInAttackStance())
        {
            agent.speed = groupMovementManager.GetSpeed();
            agent.SetDestination(groupMovementManager.gameObject.transform.position);
        }
        StartCoroutine(RegroupTimer());
    }

    private void SetTargetPosition()
    {
        if (groupMovementManager.OrderIsValid())
        {
            Vector3 targetPosition;
            Vector3 offset = transform.position - groupMovementManager.transform.position;
            Vector3 groupTargetPosition = groupMovementManager.GetLeaderTargetPosition();

            targetPosition = groupTargetPosition + offset;

            agent.ResetPath();

            if (!attackStanceManager.IsInAttackStance())
            {
                agent.speed = groupMovementManager.GetSpeed();
                agent.SetDestination(targetPosition);
            }
        }
    }
}

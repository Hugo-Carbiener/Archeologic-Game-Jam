using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;


public class AttackManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GroupMovement movementManager;
    [SerializeField] private NavMeshAgent agent;

    [Header("States")]
    [SerializeField] private bool isInattackStance;

    private void Awake()
    {
        Assert.IsNotNull(movementManager);   
        if (!agent) agent = GetComponent<NavMeshAgent>();
        isInattackStance = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleAttackStance();
            Debug.Log("Is in attack stance: " + isInattackStance);
        }
    }

    private void ToggleAttackStance()
    {
        isInattackStance = !isInattackStance;
        if (isInattackStance)
        {
            // here a filoutage happens: instead of properly denying movement in attack stance, we set speed to 0 and wil clear the navMeshAgent destination before leaving attack stance
            agent.ResetPath();
            agent.speed = 0f;
        } else
        {
            agent.ResetPath();
            agent.speed = movementManager.GetSpeed();
        }
    }
}

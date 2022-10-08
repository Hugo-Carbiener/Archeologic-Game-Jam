using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;


public class AttackStanceManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GroupMovement movementManager;
    [SerializeField] private List<NavMeshAgent> agents;

    [Header("States")]
    [SerializeField] private bool isInattackStance;

    private void Awake()
    {
        if (!movementManager) movementManager = GetComponent<GroupMovement>();  
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
        foreach(var agent in agents)
        {
            agent.SetDestination(agent.transform.position);
        }
    }

    public bool IsInAttackStance()
    {
        return isInattackStance;
    }
}

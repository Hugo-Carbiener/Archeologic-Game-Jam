using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;


public class AttackStanceManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<NavMeshAgent> agents;
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private GameObject attackAreaRangeIndicator;
    private Camera cam;

    [Header("Variables")]
    [SerializeField] private float range;
    [SerializeField] private float minAttackAreaSize;
    [SerializeField] private float maxAttackAreaSize;
    private Vector3 attackPosition;
    private float attackAreaSize;

    [Header("States")]
    [SerializeField] public bool isInattackStance = false;

    private PlayerReferencesController playerReferencesController;


    private void Awake()
    {
        attackAreaSize = minAttackAreaSize;

        if (rangeIndicator)
        {
            rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2, 1);
            rangeIndicator.SetActive(false);
        }
        if (attackAreaRangeIndicator)
        {
            attackAreaRangeIndicator.transform.localScale = new Vector3(attackAreaSize, attackAreaSize, 1);
            attackAreaRangeIndicator.SetActive(false);
        }

        // check that if we have a range indicator, we also have the other one
        if (rangeIndicator)
        {
            Assert.IsNotNull(attackAreaRangeIndicator);
        }
        if (attackAreaRangeIndicator)
        {
            Assert.IsNotNull(rangeIndicator);
        }

        cam = Camera.main;

        attackPosition = Vector3.zero;
        isInattackStance = false;

        playerReferencesController = GetComponent<PlayerReferencesController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleAttackStance();
        }
    }

    private void FixedUpdate()
    {
        //enable attack symbol in UI
        playerReferencesController.getUI().GetComponent<UiController>().UpdatesAttackStance(isInattackStance);

        if (isInattackStance && attackAreaRangeIndicator && rangeIndicator)
        {
            // detect mouse position
            Vector3 mousePosition;
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                mousePosition = hit.point;

                // refresh the position of the attack, only if we aim in range 
                float distance = Vector3.Distance(transform.position, mousePosition);
                Debug.Log("distance : " + distance);
                if (distance < range)
                {
                    attackPosition = mousePosition;
                }
                attackAreaRangeIndicator.transform.position = attackPosition + new Vector3(0, 3, 0);
                AttackAreaSizeInterpolation(attackPosition);
                attackAreaRangeIndicator.transform.localScale = new Vector3(attackAreaSize, attackAreaSize, 1);
            }
        }
    }

    private void ToggleAttackStance()
    {
        isInattackStance = !isInattackStance;
        
        foreach(var agent in agents)
        {
            agent.SetDestination(agent.transform.position);
        }

        // enable range indicator
        if (isInattackStance && rangeIndicator)
        {
            rangeIndicator.SetActive(true);
            rangeIndicator.transform.position = transform.position;
        }

        // enable attack area range indicator
        if (isInattackStance && attackAreaRangeIndicator)
        {
            attackAreaRangeIndicator.SetActive(true);
        }
    }

    /**
     * Sets the size of the attack area (and range of the indicator) depending of the mouse position 
     */
    private void AttackAreaSizeInterpolation(Vector3 mousePos)
    {
        float distance  = Vector3.Distance(transform.position, mousePos);

        attackAreaSize = minAttackAreaSize * (1 - distance/range) + maxAttackAreaSize * (distance/range);
    }

    public bool IsInAttackStance()
    {
        return isInattackStance;
    }
}

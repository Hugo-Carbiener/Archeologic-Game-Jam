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
    [SerializeField] private GameObject sagaiePrefab;
    [SerializeField] private GameObject sphere;
    private Camera cam;

    [Header("Variables")]
    [SerializeField] private float range;
    [SerializeField] private float minAttackAreaSize;
    [SerializeField] private float maxAttackAreaSize;
    private Vector3 attackPosition;
    private float attackAreaSize;

    [Header("States")]
    [SerializeField] private bool isInattackStance = false;

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
        Assert.IsNotNull(sagaiePrefab);

        // check that if we have a range indicator, we also have the other one
        if (rangeIndicator)
        {
            Assert.IsNotNull(attackAreaRangeIndicator);
        }
        if (attackAreaRangeIndicator)
        {
            Assert.IsNotNull(rangeIndicator);
        }

        sagaiePrefab.gameObject.SetActive(false);
        cam = Camera.main;

        attackPosition = Vector3.zero;
        isInattackStance = false;
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
        if (isInattackStance)
        {

            // detect mouse position
            Vector3 mousePosition;
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                mousePosition = hit.point;

                if (attackAreaRangeIndicator && rangeIndicator)
                {
                    // refresh the position of the attack, only if we aim in range 
                    float distance = Vector3.Distance(transform.position, mousePosition);
                    Debug.Log("distance : " + distance);
                    if (distance < range)
                    {
                        attackPosition = mousePosition;
                    }
                    attackAreaRangeIndicator.transform.position = attackPosition + new Vector3(0, 3, 0);
                    AttackAreaSizeInterpolation(attackPosition);
                    attackAreaRangeIndicator.transform.localScale = new Vector3(attackAreaSize * 2, attackAreaSize * 2, 1);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                // instantiate sagaie
                Instantiate(sphere, attackPosition);
                InstantiateSagaie(attackPosition);
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

    private void InstantiateSagaie(Vector3 target)
    {
        // generate Random point in circle
        float rdAngle = Random.Range(0, 360);
        float rdRadius = Random.Range(0, attackAreaSize);
        Vector3 endPoint = target + new Vector3(rdRadius * Mathf.Cos(rdAngle * Mathf.Deg2Rad), rdRadius * Mathf.Sin(rdAngle * Mathf.Deg2Rad));

        // Instantiate sagaie and give variables for parabola
        GameObject sagaie = Instantiate(sagaiePrefab);
        Sagaie sagaieComponent = sagaie.GetComponent<Sagaie>();

        sagaieComponent.SetStartPoint(transform.position);
        sagaieComponent.SetEndPoint(endPoint);
        sagaie.SetActive(true);
    }

    public bool IsInAttackStance()
    {
        return isInattackStance;
    }
}

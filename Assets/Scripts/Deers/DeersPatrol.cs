using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DeersState
{
    FixeShifting,
    Fleeing
}
public class DeersPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    private GameObject playerAgent;
    private int currentPoint;
    NavMeshAgent agent;
    private DeersState state;
    void Start()
    {
        currentPoint = 0;
        agent = GetComponent<NavMeshAgent>();
        playerAgent = GameObject.FindGameObjectWithTag("Player");
        state = DeersState.FixeShifting;

    }

    void Update()
    {
        switch (state)
        {
            case DeersState.Fleeing:
                print("fleeing");
                break;
            case DeersState.FixeShifting:
                agent.SetDestination(targetPoints[currentPoint].position);
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPoints[currentPoint].position.x, targetPoints[currentPoint].position.z)) < 1.5f)
                {
                    currentPoint++;
                    if (currentPoint >= targetPoints.Length) { currentPoint = 0; }
                }
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerAgent.transform.position.x, playerAgent.transform.position.z)) < 3f)
                {
                    state = DeersState.Fleeing;
                }

                break;
        }

    }
}

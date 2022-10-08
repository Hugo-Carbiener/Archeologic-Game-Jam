using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ephemere : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    private int currentPoint;
    NavMeshAgent agent;
    void Start()
    {
        currentPoint = 0;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(targetPoints[currentPoint].position);
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPoints[currentPoint].position.x, targetPoints[currentPoint].position.z))<1.5f)
        {
            currentPoint++;
            if (currentPoint >= targetPoints.Length) { currentPoint = 0; }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeersPatrol : MonoBehaviour
{
    [SerializeField]private Transform[] targetPoints;
    private Transform currentPoint;
    NavMeshAgent agent;
    void Start()
    {
        currentPoint = targetPoints[0];
        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(transform.position+new Vector3(3,0,0));
    }
}

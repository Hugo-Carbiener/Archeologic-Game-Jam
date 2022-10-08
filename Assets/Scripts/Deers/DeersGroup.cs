using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeersGroup : MonoBehaviour
{
    NavMeshAgent agent;
    private void Awake()
    {
        DeersPatrol.VectorToDestination += GoToo;
        DeersPatrol.Stop += OnStop;
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnDestroy()
    {
        DeersPatrol.VectorToDestination -= GoToo;
        DeersPatrol.Stop -= OnStop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoToo(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private void OnStop()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }
}

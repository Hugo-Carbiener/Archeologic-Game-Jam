using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class DeersGroup : MonoBehaviour
{
    NavMeshAgent agent;
    private void Awake()
    {
        DeersPatrol.VectorToDestination += GoToo;
        DeersPatrol.Stop += OnStop;
        DeersPatrol.ChangeSpeed += ChangeSpeed;
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnDestroy()
    {
        DeersPatrol.VectorToDestination -= GoToo;
        DeersPatrol.Stop -= OnStop;
        DeersPatrol.ChangeSpeed -= ChangeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeSpeed(float speed)
    {
        agent.speed = speed;
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public enum DeersState
{
    FixeShifting,
    Fleeing,
    WaitingForReturnInFixe
}
public class DeersPatrol : MonoBehaviour
{
    [SerializeField] float viewRange;
    [SerializeField] float fleeRange;
    [SerializeField] float timeBeforeTired;
    [SerializeField] float timeBeforeFixe;
    [SerializeField] float angleOfDeadEnd;

    [SerializeField] private Transform[] deadEnds;
    [SerializeField] private Transform[] targetPoints;
    private int currentPoint;

    private NavMeshAgent playerAgent;
    private GroupMovement groupMovement;
    NavMeshAgent agent;

    private DeersState state;

    [SerializeField] GameObject sphere;
    void Start()
    {
        currentPoint = 0;
        agent = GetComponent<NavMeshAgent>();
        playerAgent = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<NavMeshAgent>();
        groupMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GroupMovement>();
        state = DeersState.FixeShifting;

    }

    void Update()
    {
        print(state);
        switch (state)
        {
            case DeersState.Fleeing:
                agent.isStopped = true;
                agent.ResetPath();
                float relativePlayerAngularPosition = GetPlayerAngularPosition();
                //sphere.transform.position = new Vector3(-20 * Mathf.Cos(Mathf.Deg2Rad*relativePlayerAngularPosition), transform.position.y,-20 * Mathf.Sin(Mathf.Deg2Rad * relativePlayerAngularPosition));
                agent.SetDestination(ChooseDestinationOfFleeing(relativePlayerAngularPosition));   
                if(!SeeHuman())
                {
                    state = DeersState.WaitingForReturnInFixe;
                }
                break;
            case DeersState.FixeShifting:
                agent.SetDestination(targetPoints[currentPoint].position);
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPoints[currentPoint].position.x, targetPoints[currentPoint].position.z)) < 1.5f)
                {
                    currentPoint++;
                    if (currentPoint >= targetPoints.Length) { currentPoint = 0; }
                }
                
                if (SeeHuman())
                {
                    state = DeersState.Fleeing;
                }

                break;
            case DeersState.WaitingForReturnInFixe:
                agent.isStopped = true;
                agent.ResetPath();
                IEnumerator co = returnInFixe();
                StartCoroutine(co);
                if (SeeHuman())
                {
                    StopCoroutine(co);
                    state=DeersState.Fleeing;
                }
                break;
        }

    }

    private float GetPlayerAngularPosition()
    {
        Vector3 playerDirection = playerAgent.transform.position - transform.position;
        float relativePlayerAngularPosition = Mathf.Atan2(playerDirection.z, playerDirection.x) / Mathf.PI * 180;
        relativePlayerAngularPosition = relativePlayerAngularPosition%360f;
        return relativePlayerAngularPosition;
    }

    private IEnumerator returnInFixe()
    {
        currentPoint = 0;
        for (int i = 0; i < targetPoints.Length; i++)
        {
            float minDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPoints[currentPoint].position.x, targetPoints[currentPoint].position.z));
            float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetPoints[i].position.x, targetPoints[i].position.z));
            if (distance < minDistance)
            {
                currentPoint = i;
            }
        }
        yield return new WaitForSeconds(timeBeforeFixe);
        state = DeersState.FixeShifting;
    }

    private bool SeeHuman()
    {
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerAgent.transform.position.x, playerAgent.transform.position.z)) < viewRange)
        {
            return true;
        }
        return false;
    }

    private Vector3 ChooseDestinationOfFleeing(float relativePlayerAngularPosition)
    {
        Vector3 A = transform.position;
        Vector3 B = new Vector3(-fleeRange * Mathf.Cos(Mathf.Deg2Rad * (relativePlayerAngularPosition + angleOfDeadEnd)), transform.position.y, -fleeRange * Mathf.Sin(Mathf.Deg2Rad * (relativePlayerAngularPosition + angleOfDeadEnd)));
        Vector3 C = new Vector3(-fleeRange * Mathf.Cos(Mathf.Deg2Rad * (relativePlayerAngularPosition - angleOfDeadEnd)), transform.position.y, -fleeRange * Mathf.Sin(Mathf.Deg2Rad * (relativePlayerAngularPosition - angleOfDeadEnd)));
        float p = Vector3.Distance(A, B) + Vector3.Distance(B, C) + Vector3.Distance(C, A);
        float[] minDistance = new float[] { Vector3.Distance(A, B), Vector3.Distance(B, C), Vector3.Distance(C, A) };
        foreach (Transform deadEnd in deadEnds)
        {
            float distanceToEvaluate = Vector2.Distance(new Vector2(A.x,A.z), new Vector2(deadEnd.position.x, deadEnd.position.z)) 
                + Vector2.Distance(new Vector2(B.x, B.z), new Vector2(deadEnd.position.x, deadEnd.position.z)) 
                + Vector2.Distance(new Vector2(C.x, C.z), new Vector2(deadEnd.position.x, deadEnd.position.z));
            if(p/2f < distanceToEvaluate && distanceToEvaluate<p- minDistance.Min())
            {
                print("lets gooooooooooooooooooooooooooooooooooooooooooo");
                return new Vector3(deadEnd.position.x, transform.position.y, deadEnd.position.z);
            }
        }
        return new Vector3(-fleeRange * Mathf.Cos(Mathf.Deg2Rad * relativePlayerAngularPosition), transform.position.y, -fleeRange * Mathf.Sin(Mathf.Deg2Rad * relativePlayerAngularPosition));


    }

}

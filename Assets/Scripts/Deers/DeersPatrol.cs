using System;
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
    [SerializeField] float viewRangeWhenRun; //Distance à laquelle le renne voit le joueur qui court
    [SerializeField] float viewRangeWhenWalk; //Distance à laquelle le renne voit le joueur qui marche
    [SerializeField] float viewOfPlayer; //Distance à laquelle le joueur voit les rennes
    [SerializeField] float fleeRange; //Distance à laquelle le renne fuit
    [SerializeField] float timeBeforeTired; //Temps avant que le renne s'épuise
    [SerializeField] float timeBeforeFixe; //Temps avant lequelle le renne se remet à suivre son chemin
    [SerializeField] float angleOfDeadEnd; //Angle pour aller dans un cul de sac
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [SerializeField] private Transform[] deadEnds; //Liste cul de sac
    [SerializeField] private Transform[] targetPoints; //Liste point chemin
    private int currentPoint; //Point à atteindre

    private NavMeshAgent playerAgent;
    private GroupMovement groupMovement;
    NavMeshAgent agent;

    private bool isViewBePlayer; //le joueur voit le renne

    private DeersState state;

    [SerializeField] GameObject sphere;

    public static event Action<Vector3> VectorToDestination;
    public static event Action Stop;
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
        HumanSee();
        switch (state)
        {
            case DeersState.Fleeing:
                agent.isStopped = true;
                agent.ResetPath();
                Stop?.Invoke();
                agent.speed = runSpeed;

                float relativePlayerAngularPosition = GetPlayerAngularPosition();
                //sphere.transform.position = new Vector3(-20 * Mathf.Cos(Mathf.Deg2Rad*relativePlayerAngularPosition), transform.position.y,-20 * Mathf.Sin(Mathf.Deg2Rad * relativePlayerAngularPosition));
                agent.SetDestination(ChooseDestinationOfFleeing(relativePlayerAngularPosition));
                VectorToDestination?.Invoke(ChooseDestinationOfFleeing(relativePlayerAngularPosition));

                if (!SeeHuman())
                {
                    state = DeersState.WaitingForReturnInFixe;
                }
                break;
            case DeersState.FixeShifting:
                agent.speed = walkSpeed;
                agent.SetDestination(targetPoints[currentPoint].position);
                VectorToDestination?.Invoke(targetPoints[currentPoint].position);
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
                Stop?.Invoke();
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
        float viewRange = 0;
        if (groupMovement.GetMovementState() == movementStates.walking)
        {
           viewRange = viewRangeWhenWalk;
        }
        else { viewRange = viewRangeWhenRun; }
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerAgent.transform.position.x, playerAgent.transform.position.z)) < viewRange)
        {
            return true;
        }
        return false;
    }

    private void HumanSee()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerAgent.transform.position.x, playerAgent.transform.position.z)) < viewOfPlayer)
        {
            isViewBePlayer = true;
        }
        else { isViewBePlayer = false; }
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

                return new Vector3(deadEnd.position.x, transform.position.y, deadEnd.position.z);
            }
        }

        return new Vector3(-fleeRange * Mathf.Cos(Mathf.Deg2Rad * relativePlayerAngularPosition), transform.position.y, -fleeRange * Mathf.Sin(Mathf.Deg2Rad * relativePlayerAngularPosition));

    }

}

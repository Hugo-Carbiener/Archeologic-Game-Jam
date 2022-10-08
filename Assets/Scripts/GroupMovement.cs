using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum movementStates
{
    walking,
    running
}

public class GroupMovement : MonoBehaviour
{
    [Header("Components")]
    private Camera cam;

    [Header("Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runRange;
    [SerializeField] private float runSpeed;
    private float speed;
    private bool orderIsValid;
    private Vector3 targetPosition;

    [Header("States")]
    [SerializeField] private movementStates state;
    private UnityEvent onMouseRightClick;

    private void Awake()
    {
        cam = Camera.main;
        speed = walkSpeed;
        onMouseRightClick = new UnityEvent();
        targetPosition = Vector3.zero;
    }

    private void OnEnable()
    {
        onMouseRightClick.AddListener(movementOrder);
    }

    private void OnDisable()
    {
        onMouseRightClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onMouseRightClick.Invoke();
        }
    }

    private void movementOrder()
    {
        // freeze initial position
        Vector3 positionOnOrdrer = transform.position;
        orderIsValid = false;

        // detect mouse position
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            targetPosition = hit.point;
            orderIsValid = true;
        }

        if (orderIsValid)
        {
            OrderDistanceProcessing(positionOnOrdrer);
        }

    }

    private void OrderDistanceProcessing(Vector3 initialPosition)
    {
        // distance processing
        float distance = Vector3.Distance(initialPosition, targetPosition);
        if ( distance >= runRange)
        {
            Debug.Log("Cours + " + distance);
            state = movementStates.running;
            speed = runSpeed;
        } else
        {
            Debug.Log("Marche + " + distance);
            state = movementStates.walking;
            speed = walkSpeed;
        }
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public bool OrderIsValid()
    {
        return orderIsValid;
    }

    public UnityEvent getOnMouseRightClickEvent()
    {
        return onMouseRightClick;
    }
}

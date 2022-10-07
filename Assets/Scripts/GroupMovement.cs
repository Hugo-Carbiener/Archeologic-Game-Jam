using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupMovement : MonoBehaviour
{
    [Header("Components")]
    private Camera cam;

    [Header("Variables")]
    private Vector3 targetPosition;

    private UnityEvent onMouseRightClick;

    private void Awake()
    {
        cam = Camera.main;
        onMouseRightClick = new UnityEvent();
        targetPosition = Vector3.zero;
    }

    private void OnEnable()
    {
        onMouseRightClick.AddListener(SetTargetPosition);
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

    private void SetTargetPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            targetPosition = hit.point;
        }
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }

    public UnityEvent getOnMouseRightClickEvent()
    {
        return onMouseRightClick;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static IndicesControllers;

/**
 *      Basic Interactable class that will handle the detection of player in vicinity of the associated object
 *      Entirely virtual, inherited by other classes
 */
public class InteractableController : MonoBehaviour
{
    [Header("Interactable variables")]
    [SerializeField] protected float detectionRadius; //detection radius in which player is detected
    [SerializeField] private GameObject indicator;  //UI indicator of object selection


    public enum INTERACTABLE_STATES
    {
        IDLE, //by default, interactables are idle i.e. remain in undetected state. After a while, if undetected, it will emit a sound
        DETECTED, //when the interactables detect a player, they will become in the detected state, displaying an UI indication
        REVEALED //once interacted with, they enter their revealed state
    }

    public INTERACTABLE_STATES _state;

    protected void Awake()
    {
        _state = INTERACTABLE_STATES.IDLE;
    }

    protected void Update()
    {
        ScanForPlayer(); //we scan for the player - if scan successfull _states will switch to DETECTED state
        if (_state == INTERACTABLE_STATES.DETECTED && Input.GetKeyDown(KeyCode.E)) //if the player is detecting an indice AND in state = DETECTED, the indice displays the true indice i.e. the direction taken by the animal group
        {
            _state = INTERACTABLE_STATES.REVEALED;
            ActionUponDetection(); //when interacted with, the object will call an appropriate action upon detection function
        }
    }

    protected virtual void ActionUponDetection() { }


    /**
    *  function ScanForPlayer that will scan for the player instance in the immediate vicinity
    *  Called every frame
    */
    private void ScanForPlayer()
    {
        bool detected = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.tag == "Player")
            {
                detected = true;
            }
        }
        if (detected && _state != INTERACTABLE_STATES.REVEALED) //if player detected and not already displaying the revealed indice, we reveal the interactor UI element 
        {
            _state = INTERACTABLE_STATES.DETECTED;
        }
        else if (_state != INTERACTABLE_STATES.REVEALED)
        {
            _state = INTERACTABLE_STATES.IDLE;
        }
        DisplayIndicator(); //we call the function that will handle the display or not of the indicator
    }

    #region INDICATOR RELATED FUNCTIONS
    /**
     *  Function that will display or not the UI indicator that said indice can be interacted with, based on the indice's _states
     */
    protected void DisplayIndicator()
    {
        if (indicator != null && _state == INTERACTABLE_STATES.DETECTED)
        {
            indicator.SetActive(true);
        }
        else if (indicator != null)
        {
            indicator.SetActive(false);
        }
    }

    /**
     *  Function that will hide the UI indicator that said indice cannot be interacted with
     */
    protected void HideIndicator()
    {
        if (indicator != null && _state == INTERACTABLE_STATES.IDLE)
        {
            indicator.SetActive(false);
        }
    }

    #endregion

}

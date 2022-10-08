using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      This is the class used by the Interactables (indices)
 *      
 */
public class IndicesControllers : InteractableController
{
    [Header("Indice variables")]
    [SerializeField] private float timer; //timer until the indice disappears
    //[SerializeField] private float detectionRadius; //radius of detection of the players
    //[SerializeField] private GameObject indicator;  //UI indicator of object selection
    [SerializeField] private GameObject indice;
    [SerializeField] private Quaternion groupRotation; //transform of the animal group at the time of indices' spawn

    [Header("Animal references")]
    [SerializeField] private GameObject animalGroup;

    //public enum INDICES_STATES
    //{
    //    IDLE, //by default, indices are idle i.e. remain in undetected state. After a while, if undetected, it will emit a sound
    //    DETECTED, //when the indices detect a player, they will become in the detected state, displaying an UI indication
    //    REVEALED //once interacted with, they reveal a clue (revealed state)
    //}
    //public INDICES_STATES _state;

    private void Start()
    {
        //_state = INDICES_STATES.IDLE; //when spawned, indices are in an idle state until detected by player
        StartCoroutine(LifeTimer());
    }

    //private void Update()
    //{
    //    ScanForPlayer(); //we scan for the player - if scan successfull _states will switch to DETECTED state
    //    if(_state == INDICES_STATES.DETECTED && Input.GetKeyDown(KeyCode.E)) //if the player is detecting an indice AND in state = DETECTED, the indice displays the true indice i.e. the direction taken by the animal group
    //    {
    //        _state = INDICES_STATES.REVEALED;
    //        RevealIndice();
    //    }
    //}

    ///**
    // *  function ScanForPlayer that will scan for the player instance in the immediate vicinity
    // *  Called every frame
    // */
    //private void ScanForPlayer()
    //{
    //    bool detected = false;
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.transform.tag == "Player")
    //        {
    //            detected = true;
    //        }
    //    }
    //    if (detected && _state != INDICES_STATES.REVEALED) //if player detected and not already displaying the revealed indice, we reveal the interactor UI element 
    //    {
    //        _state = INDICES_STATES.DETECTED;
    //    }
    //    else if(_state != INDICES_STATES.REVEALED)
    //    {
    //        _state = INDICES_STATES.IDLE;
    //    }
    //    DisplayIndicator(); //we call the function that will handle the display or not of the indicator
    //}

    /**
     *  Function that will display the indice i.e. the trace that shows the direction taken by the animal group
     */
    private void RevealIndice()
    {
        if(_state == INTERACTABLE_STATES.REVEALED && indice!=null)
        {
            HideIndicator();
            indice.transform.rotation = groupRotation;
            indice.gameObject.SetActive(true);
        }
    }

    /**
     *      Inherited function from InteractableController, called when the player starts the interaction in the vicinity of the object
     */
    protected override void ActionUponDetection()
    {
        RevealIndice();
    }

    #region INDICATOR RELATED FUNCTIONS
    ///**
    // *  Function that will display or not the UI indicator that said indice can be interacted with, based on the indice's _states
    // */
    //private void DisplayIndicator()
    //{
    //    if(indicator != null && _state == INDICES_STATES.DETECTED)
    //    {
    //        indicator.SetActive(true);
    //    }
    //    else if (indicator != null)
    //    {
    //        indicator.SetActive(false);
    //    }
    //}

    ///**
    // *  Function that will hide the UI indicator that said indice cannot be interacted with
    // */
    //private void HideIndicator()
    //{
    //    if (indicator != null && _state == INDICES_STATES.IDLE)
    //    {
    //        indicator.SetActive(false);
    //    }
    //}
    
    #endregion

    /**
     * LifeTimer function, called by Start, that will play a sound if player has not interacted after a while
     */
    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(timer);
        if(animalGroup!=null)
        {
            //PLAY SOUND FROM THE ANIMAL GROUP
            animalGroup.GetComponent<AudioController>().PlaySound();
            print("IndicesController.LifeTimer() : Playing sound at aniamlGroup.AudioController");
            //WAIT FOR SOUND TO END
            Destroy(gameObject);
        }
    }

    #region SETTER FUNCTIONS
    /**
     * function called by the animal group script that will tell the indices a ref to the animal group (for sound calling)
     */
    public void SetAnimalGroup(GameObject group)
    {
        animalGroup = group;
    }

    /**
     * function called by the animal group script handling the spawn of indices and sets the indices' revealed direction
     */
    public void SetGroupRotation(Quaternion transform)
    {
        groupRotation = transform;
    }
    #endregion

}

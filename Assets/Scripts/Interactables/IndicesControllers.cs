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
    [SerializeField] private GameObject indice;
    [SerializeField] private Quaternion groupRotation; //transform of the animal group at the time of indices' spawn

    [Header("Animal references")]
    [SerializeField] private GameObject animalGroup;


    private void Start()
    {
        //_state = INDICES_STATES.IDLE; //when spawned, indices are in an idle state until detected by player
        StartCoroutine(LifeTimer());
    }

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

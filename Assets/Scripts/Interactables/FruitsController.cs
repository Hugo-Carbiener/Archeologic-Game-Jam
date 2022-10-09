using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IndicesControllers;

/**
 *      Component used by the fishes & fruits collected by the player that restore its stamina
 *      Inherits from the InteractablesController
 */
public class FruitsController : InteractableController
{
    [Header("Fruits & Fishes variables")]
    [SerializeField] private float staminaRestoreValue;
    [SerializeField] private string type;

    /**
     *  Function inherited from Interactable Controller, called when interacting with object.
     */
    protected override void ActionUponDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.tag == "Player")
            {
                collider.GetComponent<PlayerReferencesController>().getStamina().UpdateStamina(staminaRestoreValue);
                if (type == "baie")
                {
                    AudioManager.i.SoundOfBaie();

                }
                else
                {
                    AudioManager.i.SoundOfPoisson();
                }

            }
        }
        Destroy(gameObject);
    }
}

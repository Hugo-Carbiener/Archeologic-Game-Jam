using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaModule : MonoBehaviour
{
    [SerializeField] private float maxStamina = 20f; //maximum stamina
    [SerializeField] private float staminaWalkConsumptionRate;  //rate at which the stamina is consumed when walking
    [SerializeField] private float staminaRunConsumptionRate;  //rate at which the stamina is consumed when running - as a rule of thumb, RunRate > WalkRate
    
    private PlayerReferencesController playerReferencesController;
    private GroupMovement groupMovement;
    private movementStates playerMovementState;
    
    private float curConsumptionRate;   //current stamina consumption rate
    private float curStamina;


    private void Awake()
    {
        curStamina = maxStamina;
        curConsumptionRate = staminaWalkConsumptionRate;
        playerReferencesController = GetComponent<PlayerReferencesController>();
        groupMovement = GetComponent<GroupMovement>();
    }

    private void Start()
    {
        StartCoroutine(ConsumptionTimer());
    }

    private void Update()
    {
        playerMovementState = groupMovement.GetMovementState();
        if (playerMovementState == movementStates.walking)
        {
            curConsumptionRate = staminaWalkConsumptionRate;
        }
        else
        {
            curConsumptionRate = staminaRunConsumptionRate;
        }
    }

    /**
     *      Timer function that handles the consumption, with wait time being the consumptionRate
     */
    private IEnumerator ConsumptionTimer()
    {
        yield return new WaitForSeconds(curConsumptionRate);
        ConsumeStamina();
        StartCoroutine(ConsumptionTimer());
    }

    /**
     *  Function that handles the consumption of the stamina bar
     */
    private void ConsumeStamina()
    {
        curStamina-=1f;
        playerReferencesController.getUI().GetComponent<UiController>().UpdateStaminaBar(curStamina/maxStamina);
        if (curStamina <= 0)
        {
            curStamina = 0;
            Death();
        }
    }

    public void UpdateStamina(float value)
    {
        if (curStamina + value < maxStamina)
        {
            curStamina += value;
        }
        else
        {
            curStamina = maxStamina;
        }
    }

    /**
     *  Death function called when out of stamina
     */
    private void Death()
    {
        AudioManager.SetDeerAudioState(deerStates.Dead);
        Destroy(gameObject);
    }
}

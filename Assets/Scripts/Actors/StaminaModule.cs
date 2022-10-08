using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaModule : MonoBehaviour
{
    [SerializeField] private float maxStamina = 20f; //maximum stamina
    [SerializeField] private float staminaConsumptionRate;  //rate at which the stamina is consumed (can change depending)
    private PlayerReferencesController playerReferencesController;
    private float curStamina;


    private void Awake()
    {
        curStamina = maxStamina;
        playerReferencesController = GetComponent<PlayerReferencesController>();
    }

    private void Start()
    {
        StartCoroutine(ConsumptionTimer());
    }

    private IEnumerator ConsumptionTimer()
    {
        yield return new WaitForSeconds(staminaConsumptionRate);
        ConsumeStamina();
        StartCoroutine(ConsumptionTimer());
    }

    /**
     *  Function that handles the consumption of the stamina bar
     */
    private void ConsumeStamina()
    {
        print(curStamina);
        curStamina-=1f;
        playerReferencesController.getUI().GetComponent<UiController>().UpdateStaminaBar(curStamina/maxStamina);
        if (curStamina <= 0)
        {
            curStamina = 0;
            Death();
        }
    }

    /**
     *  Death function called when out of stamina
     */
    private void Death()
    {
        Destroy(gameObject);
    }
}

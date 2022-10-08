using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/**
 *      Component used by the UI prefab
 *  Handles the various methods of the UI, including the stamina bar and attack symbol
 */
public class UiController : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Image attackSymbol;

    private void Awake()
    {
        Assert.IsNotNull(staminaBar);
        Assert.IsNotNull(attackSymbol);
    }


    /**
     *      Function called by the StaminaModule of the player, that will update the slider value
     */
    public void UpdateStaminaBar(float value)
    {
        staminaBar.value = value;
    }

    /**
     *      Function called by AttackStanceManager of the player, that will update the attack status
     */
    public void UpdatesAttackStance(bool isInAttackStance)
    {
        if (isInAttackStance) //if player in attack stance
        {
            attackSymbol.gameObject.SetActive(true);
        }
        else
        {
            attackSymbol.gameObject.SetActive(false);
        }
    }
}

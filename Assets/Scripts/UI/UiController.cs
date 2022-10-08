using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/**
 *      Component used by the UI prefab
 *  Handles the various methods of the UI, including the stamina bar
 */
public class UiController : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;

    private void Awake()
    {
        Assert.IsNotNull(staminaBar);
    }


    /**
     *      Function called by the StaminaModule of the player, that will update the slider value
     */
    public void UpdateStaminaBar(float value)
    {
        staminaBar.value = value;
    }
}

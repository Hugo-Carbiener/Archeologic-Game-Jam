using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

/**
 *      Component used by the UI prefab
 *  Handles the various methods of the UI, including the stamina bar and attack symbol
 */

public enum stateGame
{
    start,
    ingame,
    win,
    lost
}
public class UiController : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Image attackSymbol;
    [SerializeField] private GameObject UIOfStart;
    [SerializeField] private GameObject UIOfWin;
    [SerializeField] private GameObject UIOfLost;
    [SerializeField] private GameObject UIOfGame;
    public stateGame state;

    private void Awake()
    {
        Assert.IsNotNull(staminaBar);
        Assert.IsNotNull(attackSymbol);
        state = stateGame.start;

        Sagaie.endGame += Win;
    }

    private void OnDestroy()
    {
        Sagaie.endGame -= Win;
    }


    /**
     *      Function called by the StaminaModule of the player, that will update the slider value
     */
    public void UpdateStaminaBar(float value)
    {
        staminaBar.value = value;

        if (value == 0)
        {

            state = stateGame.lost;
        }
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

    public void GoInGame()
    {
        state = stateGame.ingame;
        AudioManager.i.SoundClick();
        }

    private void Update()
    {
        if (state == stateGame.start)
        {
            UIOfGame.SetActive(false);
            UIOfLost.SetActive(false);
            UIOfStart.SetActive(true);
            UIOfWin.SetActive(false);
            Time.timeScale = 0;
        }
        if (state == stateGame.ingame)
        {
            UIOfGame.SetActive(true);
            UIOfLost.SetActive(false);
            UIOfStart.SetActive(false);
            UIOfWin.SetActive(false);
            Time.timeScale = 1;

        }
        if(state == stateGame.win)
        {
            UIOfGame.SetActive(false);
            UIOfLost.SetActive(false);
            UIOfStart.SetActive(false);
            UIOfWin.SetActive(true);
            print("j'ai gagn�");
            Time.timeScale = 0;
        }
        if (state == stateGame.lost)
        {
            UIOfGame.SetActive(false);
            UIOfLost.SetActive(true);
            UIOfStart.SetActive(false);
            UIOfWin.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void Win()
    {
        state = stateGame.win;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Component used by the animal group to launch localized sounds
 */
public class AudioController : MonoBehaviour
{
    //function called by the Indices' controller that will play a faraway sound
    public void PlaySound()
    {
        //PLAY SOUND
        print("Audio Controller.PlaySound() : playing sound");
    }
}

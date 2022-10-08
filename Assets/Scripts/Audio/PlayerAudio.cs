using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodMusicEvent;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodMusicEvent);
        instance.start();
    }

    public void SetWalkAudio()
    {
        instance.setParameterByName("Run Pelo", 0);
    }

    public void SetRunAudio()
    {
        instance.setParameterByName("Run Pelo", 1);
    }

    public void SetNoFootsteps()
    {

        instance.setParameterByName("Run Pelo", 2);
    }
}

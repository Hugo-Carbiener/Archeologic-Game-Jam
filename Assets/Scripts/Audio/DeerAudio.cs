using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAudio : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.StudioEventEmitter fmodMusicEventEmitter;

    public FMODUnity.EventReference fmodAudioEvent;

    void Start()
    {
        fmodAudioEvent = fmodMusicEventEmitter.EventReference;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodAudioEvent);
        instance.start();
    }

    public void SetWalkAudio()
    {
        instance.setParameterByName("Run Deer", 0);
    }

    public void SetRunAudio()
    {
        instance.setParameterByName("Run Deer", 1);
    }

    public void SetNoFootsteps()
    {
        instance.setParameterByName("Run Deer", 2);
    }
}

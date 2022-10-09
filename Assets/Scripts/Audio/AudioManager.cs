using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum deerStates
{
    Idle,
    Spotted,
    Run,
    Dead
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager i { get; private set; }

    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance indiceInstance;
    private FMOD.Studio.EventInstance baieInstance;
    private FMOD.Studio.EventInstance poissonInstance;

    public FMODUnity.EventReference fmodMusicEvent;
    public FMODUnity.EventReference fmodIndiceEvent;
    public FMODUnity.EventReference fmodBaieEvent;
    public FMODUnity.EventReference fmodPoissonEvent;

    private int indiceAmount = 1;

    public bool deathMusicIsPlaying = false;


    void Start()
    {
        if (i != null && i != this)
            Destroy(gameObject);
        i = this;

        instance = FMODUnity.RuntimeManager.CreateInstance(fmodMusicEvent);
        instance.start();
        indiceInstance= FMODUnity.RuntimeManager.CreateInstance(fmodIndiceEvent);
        poissonInstance= FMODUnity.RuntimeManager.CreateInstance(fmodPoissonEvent);
        baieInstance = FMODUnity.RuntimeManager.CreateInstance(fmodBaieEvent);
    }

    public void AddIndice()
    {
        indiceAmount++;
        if (indiceAmount > 7)
        {
            indiceAmount = 7;
        }
        if (indiceAmount < 1)
        {
            indiceAmount = 1;
        }

        Debug.Log(indiceAmount);

        instance.setParameterByName("Indices", indiceAmount);
    }

    public static void SetDeerAudioState(deerStates state)
    {
        i.instance.setParameterByNameWithLabel("Deerstate", state.ToString());
        if (state == deerStates.Dead)
        {
            i.deathMusicIsPlaying = true;
        }
    } 

    public void SoundOfIndice()
    {
        i.indiceInstance.start();
    }

    public void SoundOfBaie()
    {
        i.baieInstance.start();
    }

    public void SoundOfPoisson()
    {
        i.poissonInstance.start();
    }
}

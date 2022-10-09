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
    private FMOD.Studio.EventInstance heartBeatInstance;
    private FMOD.Studio.EventInstance menuClickInstance;


    public FMODUnity.EventReference fmodMusicEvent;
    public FMODUnity.EventReference fmodIndiceEvent;
    public FMODUnity.EventReference fmodBaieEvent;
    public FMODUnity.EventReference fmodPoissonEvent;
    public FMODUnity.EventReference fmodheartBeatEvent;
    public FMODUnity.EventReference fmodmenuclickEvent;


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
        heartBeatInstance= FMODUnity.RuntimeManager.CreateInstance(fmodheartBeatEvent);
        heartBeatInstance.start();
        menuClickInstance = FMODUnity.RuntimeManager.CreateInstance(fmodmenuclickEvent);
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

    public void ChangeStamina(float stamina, float maxStamina)
    {
        heartBeatInstance.setParameterByName("Stamina", stamina/maxStamina);
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

    public void SoundClick()
    {
        i.menuClickInstance.start();
    }

}

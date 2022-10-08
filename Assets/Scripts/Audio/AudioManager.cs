using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i { get; private set; }

    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodMusicEvent;

    private int indiceAmount = 1;


    void Start()
    {
        if (i != null && i != this)
            Destroy(gameObject);
        i = this;

        instance = FMODUnity.RuntimeManager.CreateInstance(fmodMusicEvent);
        instance.start();
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
}

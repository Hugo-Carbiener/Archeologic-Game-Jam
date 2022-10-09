using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Component used to randomly select skins
 * 
 */
public class SkinSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;

    private void Awake()
    {
        skins[Random.Range(0, skins.Length)].SetActive(true);
    }
}

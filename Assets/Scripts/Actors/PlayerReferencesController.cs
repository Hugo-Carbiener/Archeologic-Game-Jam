using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Component contained by the player that will handle all the references to external objects
 *      
 */
public class PlayerReferencesController : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private GameObject ui;
    public GameObject getUI() { return ui; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IndicesControllers;

/**
 *      Component used by the fishes & fruits collected by the player that restore its stamina
 * 
 */
public class FruitsController : MonoBehaviour
{
    [SerializeField] private float staminaRestoreValue;
    [SerializeField] private float detectionRadius;

    public enum FRUITS_STATES
    {
        IDLE,
        DETECTED,
    }

    public FRUITS_STATES _state;

    private void Awake()
    {
        _state = FRUITS_STATES.IDLE;
    }

    private void Update()
    {
        //ScanForPlayer();
    }


}

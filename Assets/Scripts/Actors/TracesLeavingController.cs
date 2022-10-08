using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Component used by the animals that will periodically leave traces/indices behind
 *      The traces are left at regular intervals of time, with a small random value
 */
public class TracesLeavingController : MonoBehaviour
{
    [SerializeField] private GameObject indice; //the indices left behind by the animals
    [SerializeField] private float timer; //timer between 2 spawn of indices

    private void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    /**
     *      At regular intervals, the animals will leave a trace behind them
     *      Small random value added on top for flavor
     */
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(timer + Random.Range(1,5));
        SpawnIndice();
        StartCoroutine(SpawnTimer());
    }

    /**
     *  Function that will handle the spawn of the indice (called when spawn is needed)
     */
    private void SpawnIndice()
    {
        GameObject new_indice = Instantiate(indice);
        new_indice.transform.position = gameObject.transform.position;
        new_indice.SetActive(true);
        if (new_indice.GetComponent<IndicesControllers>() != null)
        {
            new_indice.GetComponent<IndicesControllers>().SetAnimalGroup(gameObject);
            new_indice.GetComponent<IndicesControllers>().SetGroupRotation(gameObject.transform.rotation);
        }
    }
}

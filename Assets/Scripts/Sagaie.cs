using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SagaieState
{
    midAir,
    inGround
}

public class Sagaie : MonoBehaviour
{
    [Header("Trajectory variables")]
    [SerializeField] private float duration;
    private Vector3 start;
    private Vector3 end;
    private SagaieState state;

    private void Awake()
    {
        if (start == null || end == null)
        {
            Debug.Log("Nik ses morts");
        }
        state = SagaieState.inGround;

        StartCoroutine(Fly(start, end));
        print("start " + start);
        print("end " + end);
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public IEnumerator Fly(Vector3 start, Vector3 finish) //Coroutine calcul de la trajectoire
    {

        state = SagaieState.midAir;

        float animation = 0f;

        while (animation < duration)
        {
            animation += Time.deltaTime;

            //lancement du joueur selon une parabole
            transform.position = Parabola(start, finish, duration, animation / duration);

            yield return null;
        }

        state = SagaieState.inGround;
        yield return null;
    }

    

    public void SetStartPoint(Vector3 start)
    {
        this.start = start;
    }
    public void SetEndPoint(Vector3 end)
    {
        this.end = end;
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }
}

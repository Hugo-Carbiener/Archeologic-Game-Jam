using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ephemereplayerdeplacement : MonoBehaviour
{
    float speed = 20;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("z"))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            pos.x -= speed * Time.deltaTime;
        }


        transform.position = pos;
    }
}

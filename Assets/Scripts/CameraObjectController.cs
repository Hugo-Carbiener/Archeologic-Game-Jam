using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Function that will enable the camera following the group without rotation
 */
public class CameraObjectController : MonoBehaviour
{
    [SerializeField] private GameObject group;
    private float offset_y;

    private void Start()
    {
        offset_y = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(group.transform.position.x, offset_y, group.transform.position.z - 12.6f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMount : MonoBehaviour
{
    public Transform cameraPos;
    void FixedUpdate()
    {
        transform.position = cameraPos.position;
    }
}

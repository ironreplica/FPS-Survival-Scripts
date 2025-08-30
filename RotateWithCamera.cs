using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    public Transform camera;
    public Vector3 weaponOffset;
    public float smoothTime = 0.1f;

    private Vector3 curVelocity;

    private void LateUpdate()
    {
        Vector3 targetPos = camera.position + camera.TransformVector(weaponOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref curVelocity, smoothTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, camera.rotation, Time.deltaTime / smoothTime);
    }
}

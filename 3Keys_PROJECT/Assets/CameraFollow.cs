using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float CameraOffset = -10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, CameraOffset);
    }
}

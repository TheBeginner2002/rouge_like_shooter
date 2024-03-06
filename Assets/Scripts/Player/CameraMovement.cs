using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Update() 
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = target.position + offset;
    }
}

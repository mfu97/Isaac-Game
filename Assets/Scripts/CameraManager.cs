using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    private void Start()
    {
        locationOffset = transform.position;
    }
    void Update()
    {
        {
            Vector3 desiredPosition = target.position + locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

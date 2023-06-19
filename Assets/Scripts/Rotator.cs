using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Vector3 rotationAxis = Vector3.up;
    private void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed);
    }
}

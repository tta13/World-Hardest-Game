using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotationRate = 50.0f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationRate * Time.fixedDeltaTime);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRotate : MonoBehaviour
{
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    public float rotationSpeed = 45f; // Adjust this value to set the rotation speed

    void Update()
    {
        float newRotX = rotateX ? Time.time * rotationSpeed : transform.rotation.eulerAngles.x;
        float newRotY = rotateY ? Time.time * rotationSpeed : transform.rotation.eulerAngles.y;
        float newRotZ = rotateZ ? Time.time * rotationSpeed : transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(newRotX, newRotY, newRotZ);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHover : MonoBehaviour
{
    public bool modeX = true;
    public bool modeY = true;
    public bool modeZ = true;

    public float hoverHeight = 0.5f;
    public float hoverSpeed = 2f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float newX = modeX ? initialPosition.x + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight : transform.position.x;
        float newY = modeY ? initialPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight : transform.position.y;
        float newZ = modeZ ? initialPosition.z + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight : transform.position.z;

        transform.position = new Vector3(newX, newY, newZ);
    }
}

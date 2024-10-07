using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float unitsPerSecond = 0.0f;

    public Vector3 origin = Vector3.zero;
    public Vector3 destination = Vector3.zero;

    float elapsedTime = 0.0f;

    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        if (transform.position != destination)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(origin, destination, elapsedTime / ((destination - origin).magnitude / unitsPerSecond));
        }
    }

    public void ReverseMotion()
    {
        Vector3 newOrigin = destination;

        destination = origin;
        origin = newOrigin;

        elapsedTime = 0.0f;
    }
}

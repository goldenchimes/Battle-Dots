using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Collector : MonoBehaviour
{
    Base team;

    Vector3 collectionPosition;

    Mover mover;

    void Awake()
    {
        collectionPosition = transform.position;

        mover = GetComponent<Mover>();
    }

    void Start()
    {
        mover.destination = ((ResourceField) FindObjectOfType(typeof(ResourceField))).transform.position;
    }

    void LateUpdate()
    {
        if (transform.position == mover.destination)
        {
            if (mover.destination == collectionPosition)
            {
                team.OnResourceCollected();
            }

            mover.ReverseMotion();
        }
    }

    void SetTeam(Base newTeam)
    {
        team = newTeam;
    }
}

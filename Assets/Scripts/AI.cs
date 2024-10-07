using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class AI : MonoBehaviour
{
    protected Base team;

    void Awake()
    {
        team = GetComponent<Base>();
    }
}

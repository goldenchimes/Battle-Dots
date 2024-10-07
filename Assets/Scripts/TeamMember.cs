using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMember : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer colorRenderer;

    [SerializeField]
    int health = 100;

    public Color color
    {
        set
        {
            colorRenderer.color = value;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

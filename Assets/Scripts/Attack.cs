using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Attack : MonoBehaviour
{
    [SerializeField]
    int damage = 10;

    [SerializeField]
    float duration = 0.0f;

    [SerializeField]
    float cooldown = 1.0f;

    float elapsedTime = 0.0f;

    List<Collider2D> hits = new List<Collider2D>();

    SpriteRenderer display;
    Collider2D trigger;

    bool coolingDown = false;

    void Awake()
    {
        display = GetComponent<SpriteRenderer>();
        trigger = GetComponent<Collider2D>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (coolingDown)
        {
            if (elapsedTime >= cooldown)
            {
                coolingDown = false;

                display.enabled = true;
                trigger.enabled = true;

                elapsedTime = 0.0f;
            }
        }
        else if (elapsedTime >= duration)
        {
            coolingDown = true;

            display.enabled = false;
            trigger.enabled = false;

            elapsedTime = 0.0f;
        }
    }

    void FixedUpdate()
    {
        hits.Clear();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (hits.IndexOf(collider) < 0)
        {
            hits.Add(collider);

            collider.GetComponent<TeamMember>()?.TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UnitFactory : MonoBehaviour
{
    [SerializeField]
    GameObject unit;

    [SerializeField]
    GameObject attack;

    [SerializeField]
    float secondsPerProduction;

    [SerializeField]
    public GameObject target;

    [SerializeField]
    bool canSetTarget = true;

    float elapsedTime = 0.0f;

    SpriteRenderer display;
    float alpha;

    public Base owner;

    bool producing = false;

    void Awake()
    {
        display = GetComponent<SpriteRenderer>();

        alpha = display.color.a;

        ChangeAlpha(alpha / 2);

        target.GetComponent<UnitTarget>().WatchClick(this);
    }

    void Update()
    {
        if (producing && !target.activeInHierarchy)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= secondsPerProduction)
            {
                GameObject newUnit = Instantiate(unit, transform.position, Quaternion.identity);

                newUnit.SendMessage("SetTeam", owner, SendMessageOptions.DontRequireReceiver);
                newUnit.GetComponent<Mover>().destination = target.transform.position;
                newUnit.layer = owner.gameObject.layer;

                if (attack)
                {
                    GameObject newAttack = Instantiate(attack, newUnit.transform);

                    newAttack.layer = owner.gameObject.layer;
                }

                elapsedTime = 0.0f;

                SetProducing(false);

                owner.NewUnit(newUnit.GetComponent<TeamMember>());
            }
        }
    }

    public void SetProducing(bool newProducing)
    {
        if (producing != newProducing)
        {
            producing = newProducing;

            if (producing)
            {
                ChangeAlpha(alpha);

                elapsedTime = 0.0f;
            }
            else
            {
                ChangeAlpha(alpha / 2);
            }

            if (canSetTarget)
            {
                target.SetActive(producing);
            }
        }
    }

    void ChangeAlpha(float newAlpha)
    {
        Color color = display.color;

        color.a = newAlpha;

        display.color = color;
    }

    public void OnUnitTargetClicked()
    {
        target.SetActive(false);
    }

    public void OnUnitTargetCanceled()
    {
        SetProducing(false);
    }
}

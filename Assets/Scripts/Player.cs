using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Base))]
public class Player : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI resourceDisplay;

    [SerializeField]
    Button factoryButton;

    [SerializeField]
    UnitFactory collectorFactory;

    [SerializeField]
    UnitFactory[] offenseFactories;

    void Start()
    {
        GetComponent<Base>().WatchResources(this);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HoveredFactory()?.SetProducing(true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            HoveredFactory()?.SetProducing(false);
        }
    }

    UnitFactory HoveredFactory()
    {
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, LayerMask.GetMask("Unit Factory")))
        {
            if (hit.rigidbody.gameObject == collectorFactory.gameObject)
            {
                return collectorFactory;
            }
            
            foreach (UnitFactory factory in offenseFactories)
            {
                if (hit.rigidbody.gameObject == factory.gameObject)
                {
                    return factory;
                }
            }
        }

        return null;
    }

    public void OnResourcesChanged(int resources)
    {
        resourceDisplay.SetText(resources.ToString());

        if (factoryButton)
        {
            factoryButton.interactable = resources >= 10;
        }
    }

    public void AllFactoriesAvailable()
    {
        Destroy(factoryButton.gameObject);
    }

    void OnDestroy()
    {
        // TODO: Give game over message and button to reload scene.
    }
}

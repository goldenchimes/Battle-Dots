using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class Base : MonoBehaviour
{
    [SerializeField]
    Color color;

    [SerializeField]
    public int resources = 5;

    [SerializeField]
    public UnitFactory collectorFactory;

    [SerializeField]
    public List<UnitFactory> unusedFactories = new List<UnitFactory>();

    Player player;

    void Awake()
    {
        GetComponent<TeamMember>().color = color;

        foreach (UnitFactory factory in GetComponentsInChildren<UnitFactory>(true))
        {
            factory.owner = this;
        }
    }

    public void NewUnit(TeamMember unit)
    {
        if (resources > 0)
        {
            unit.color = color;

            UpdateResources(-1);
        }
        else
        {
            Destroy(unit.gameObject);
        }
    }

    public void OnResourceCollected()
    {
        UpdateResources(1);
    }

    void UpdateResources(int delta)
    {
        resources += delta;

        player?.OnResourcesChanged(resources);
    }

    public void WatchResources(Player newPlayer)
    {
        player = newPlayer;

        UpdateResources(0);
    }

    public void NewFactory()
    {
        if (resources >= 10 && unusedFactories.Count > 0)
        {
            UnitFactory factory = unusedFactories[0];

            UpdateResources(-10);

            factory.gameObject.SetActive(true);

            unusedFactories.RemoveAt(0);

            if (unusedFactories.Count == 0)
            {
                player?.AllFactoriesAvailable();
            }
        }
    }

    public UnitFactory NewAIFactory()
    {
        if (resources >= 10 && unusedFactories.Count > 0)
        {
            UnitFactory factory = unusedFactories[0];

            UpdateResources(-10);

            factory.gameObject.SetActive(true);

            unusedFactories.RemoveAt(0);

            if (unusedFactories.Count == 0)
            {
                player?.AllFactoriesAvailable();
            }

            return factory;
        }

        return null;
    }
}

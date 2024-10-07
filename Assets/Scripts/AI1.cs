using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI1 : AI
{
    List<UnitFactory> factories = new List<UnitFactory>();

    float elapsedTime = 0.0f;
    float delay = 0.0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > delay)
        {
            List<Object> options = new List<Object>(FindObjectsOfType(typeof(Base)));
            
            options.AddRange(FindObjectsOfType(typeof(Mover)));

            for (int i = options.Count - 1; i >= 0; i--)
            {
                if (((MonoBehaviour) options[i]).gameObject.layer == gameObject.layer)
                {
                    options.RemoveAt(i);
                }
            }

            if (options.Count > 0)
            {
                if (team.resources > 1)
                {
                    team.collectorFactory.SetProducing(true);
                }

                foreach (UnitFactory factory in factories)
                {
                    if (team.resources > 1 && options.Count > 0)
                    {
                        int i = Random.Range(0, options.Count);

                        factory.SetProducing(true);
                        factory.target.transform.position = ((MonoBehaviour) options[i]).gameObject.transform.position;
                        factory.OnUnitTargetClicked();

                        options.RemoveAt(i);
                    }
                }

                if (team.resources > 10 && team.unusedFactories.Count > 0)
                {
                    UnitFactory factory = team.NewAIFactory();

                    if (factory)
                    {
                        factories.Add(factory);
                    }
                }

                delay = Random.value * 5;
                elapsedTime = 0.0f;
            }
        }
    }
}

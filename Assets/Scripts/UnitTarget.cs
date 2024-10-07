using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTarget : MonoBehaviour
{
    UnitFactory watcher;

    void Update()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);

            watcher.OnUnitTargetClicked();
        }

        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);

            watcher.OnUnitTargetCanceled();
        }
    }

    public void WatchClick(UnitFactory newWatcher)
    {
        watcher = newWatcher;
    }
}

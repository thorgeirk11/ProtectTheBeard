using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeardController : MonoBehaviour
{
    public GameObject beardObj;
    public static BeardController instance;
    public bool Dead { get; private set; }

    private Dictionary<Transform, Vector3> defaultLocations;
    public List<WizzardBeardSphere> WizzardBeard;

    // Use this for initialization
    void Awake()
    {
        WizzardBeard = new List<WizzardBeardSphere>();
        defaultLocations = new Dictionary<Transform, Vector3>();
        Dead = false;
        foreach (Transform item in beardObj.transform)
        {
            WizzardBeard.Add(item.GetComponent<WizzardBeardSphere>());
            defaultLocations[item] = item.position;
        }
        instance = this;
    }

    internal void RestoreBeard(WizzardBeardSphere wizzardBeardSphere)
    {
        wizzardBeardSphere.transform.SetParent(beardObj.transform);
        var position = defaultLocations[wizzardBeardSphere.transform];
        iTween.MoveTo(wizzardBeardSphere.gameObject, position, 2f);
    }

    public List<WizzardBeardSphere> StealWizzardsBeard(int howMany)
    {
        var stolen = new List<WizzardBeardSphere>();
        for (int i = WizzardBeard.Count - 1; i >= 0; i--)
        {
            if (howMany == 0) break;
            if (!WizzardBeard[i].HasBeenPickedUp)
            {
                stolen.Add(WizzardBeard[i]);
                howMany--;
            }
        }
        if (howMany > 0) Dead = true;
        return stolen;
    }

}

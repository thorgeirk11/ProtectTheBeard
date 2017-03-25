using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardController : MonoBehaviour {

    public GameObject beard;
    public List<Transform> beardparts;
    public static BeardController instance;
    public bool Dead { get; private set; }
    // Use this for initialization
    void Awake()
    {
        beardparts = new List<Transform>();
        Dead = false;
        foreach (Transform item in beard.transform)
        {
            beardparts.Add(item);
        }
        instance = this;
    }

    public void RemovePartOfBeard(int howMany)
    {
        for(int i = beardparts.Count-1; i >= 0; i--)
        {
            if (howMany == 0) break;
            if (beardparts[i].GetComponent<Renderer>().enabled)
            {
                beardparts[i].GetComponent<Renderer>().enabled = false;
                howMany--;
            }
        }
        if (howMany > 0) Dead = true;
    }

}

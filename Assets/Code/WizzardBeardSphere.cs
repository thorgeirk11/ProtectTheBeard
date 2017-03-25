using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WizzardBeardSphere : MonoBehaviour
{
    public bool IsOnEnemy { get; set; }
    public bool HasBeenPickedUp { get; set; }

    void OnTriggerEnter(Collider entering)
    {
        print(entering.name + " touched Beard Sphere");
        if (HasBeenPickedUp && !IsOnEnemy &&
            entering.tag == "Player")
        {
            BeardController.instance.RestoreBeard(this);
            HasBeenPickedUp = false;
        }
    }


    // Use this for initialization
    void Start()
    {
        IsOnEnemy = false;
        HasBeenPickedUp = false;
    }
}

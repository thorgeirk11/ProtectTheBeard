using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WizzardBeardSphere : MonoBehaviour
{
    private Collider colider;
    private Light light;

    public bool IsOnEnemy { get; private set; }
    public bool HasBeenPickedUp { get; private set; }

    // Use this for initialization
    void Start()
    {
        light = GetComponent<Light>();
        colider = GetComponent<Collider>();
        IsOnEnemy = false;
        HasBeenPickedUp = false;
    }

    public void EnemyPickingUp()
    {
        light.enabled = true;
        IsOnEnemy = true;
        HasBeenPickedUp = true;
    }
    internal void EnemyDroped()
    {
        colider.enabled = true;
        light.enabled = true;
        transform.position = new Vector3(transform.position.x, .25f, transform.position.z);
        IsOnEnemy = false;
    }

    void OnTriggerEnter(Collider entering)
    {
        //print(entering.name + " touched Beard Sphere");
        if (HasBeenPickedUp && !IsOnEnemy &&
            entering.tag == "Player")
        {
            light.enabled = false;
            BeardController.instance.RestoreBeard(this);
            HasBeenPickedUp = false;
        }
    }

}

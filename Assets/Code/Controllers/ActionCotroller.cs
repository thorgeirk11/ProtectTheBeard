﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ActionCotroller : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject obstaclePrefab;
    public GameObject oilPrefab;

    private PlayerController player;
    private ActionBar actionBar;
    private BeardOilBar oilBar;
    public void Start()
    {
        player = FindObjectOfType<PlayerController>();
        actionBar = FindObjectOfType<ActionBar>();
        oilBar = FindObjectOfType<BeardOilBar>();
    }

    public void PerformAction(ActionKey key)
    {
        var action = actionBar.ActionOnKey(key);

        if (BeardOilBar.OilAmount < action.Cost)
        {
            print("You do not have enough oil");
            return;
        }
        oilBar.UseOil(action.Cost);
        switch (action.Type)
        {
            case ActionType.GrowBeard:
                CastGrowBeard();
                break;
            case ActionType.ThrowOil:
                CastOilSpill();
                break;
            case ActionType.RestoreBeard:
                print("RestoreBeard Is not implemented Yet!");
                break;
            case ActionType.Obstacle:
                CastObstacle();
                break;
            default:
                break;
        }
    }

    public void CastOilSpill()
    {
        SoundManager.Instance.PlayOilSpellSound();
        Plane plane = new Plane(Vector3.up, -0.5f);
        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Instantiate(oilPrefab, point, Quaternion.identity);
        }
    }
    public void CastGrowBeard()
    {
        SoundManager.Instance.PlayPewPewSound();
        var pos = player.transform.position;
        Instantiate(bulletPrefab, pos, player.transform.rotation);
    }

    public void CastObstacle()
    {
        SoundManager.Instance.PlayWallSpellSound();
        Plane plane = new Plane(Vector3.up, -0.5f);
        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Instantiate(obstaclePrefab, point, Quaternion.identity);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ActionCotroller : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject obstaclePrefab;

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
                print("Throw Oil Is not implemented Yet!");
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


    public void CastGrowBeard()
    {
        var pos = player.transform.position;
        Instantiate(bulletPrefab, pos, player.transform.rotation);
    }

    public void CastObstacle()
    {
        Plane plane = new Plane(Vector3.up, -0.5f);

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Instantiate(obstaclePrefab, point, player.transform.rotation);
        }
    }

}

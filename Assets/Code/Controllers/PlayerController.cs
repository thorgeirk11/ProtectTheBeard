using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    [Range(1, 100)]
    public float speed;

    ActionCotroller Actions;
    Rigidbody body;
    Renderer rend;
    Color normalColor;
    private float minZ = -6, maxZ = 20;
    private float minX = -20, maxX = 0;
    readonly Dictionary<KeyCode[], Vector3> Directions = new Dictionary<KeyCode[], Vector3>
    {
        { new[] { KeyCode.W, KeyCode.UpArrow   }, Vector3.forward   },
        { new[] { KeyCode.S, KeyCode.DownArrow }, Vector3.back  },
        { new[] { KeyCode.A, KeyCode.LeftArrow }, Vector3.left  },
        { new[] { KeyCode.D, KeyCode.RightArrow}, Vector3.right },
    };

    // Use this for initialization
    void Start()
    {
        Actions = GameController.Instance.GetComponent<ActionCotroller>();
        body = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        normalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.timestopped)
        {
            HandleMovement();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Actions.PerformAction(ActionKey.LeftMouse);

            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Actions.PerformAction(ActionKey.RightMouse);
                
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Actions.PerformAction(ActionKey.Q);
                
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (GameObject dest in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    dest.GetComponent<Enemy>().gotHit();
                }
                foreach (GameObject dest in GameObject.FindGameObjectsWithTag("Obstacle"))
                {
                    //Destroy(dest);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopTime();
        }
    }

    private void HandleMovement()
    {
        //var isMoving = false;
        foreach (var dir in Directions)
        {
            foreach (var key in dir.Key)
            {
                if (Input.GetKey(key))
                {
                    body.AddForce(dir.Value * Time.deltaTime * speed, ForceMode.Impulse);
                    //isMoving = true;
                    break;
                }
            }
        }
        //if (!isMoving)
        //{
        //    body.velocity = Vector3.zero;
        //}
    }
    private void StopTime()
    {
        GameController.Instance.stopTime();
    }
}

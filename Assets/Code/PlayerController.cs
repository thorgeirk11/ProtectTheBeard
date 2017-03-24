using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    [Range(1, 100)]
    public float speed;
    public GameObject bulletPrefab;
    public GameObject obstaclePrefab;
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
        body = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        GameController.Instance.Player = this;
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
                Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Plane plane = new Plane(Vector3.up, -0.5f);
                
                float dist;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out dist))
                {
                    Vector3 point = ray.GetPoint(dist);
                    Instantiate(obstaclePrefab, point, transform.rotation);
                }
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
    internal void Damage()
    {
        GameController.Instance.PlayerGotHit();
        rend.material.color = Color.red;
        Wait(.5f, () => rend.material.color = normalColor);
    }
}

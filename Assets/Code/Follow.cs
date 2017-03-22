using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Transform follow;

    Transform tran;
    private float y;
    private float z;

    // Use this for initialization
    void Start()
    {
        follow = FindObjectOfType<PlayerController>().transform;
        tran = transform;
        y = tran.position.y;
        z = tran.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        var x = iTween.FloatUpdate(tran.position.x, follow.position.x, 10);
        transform.position = new Vector3(x, y, z);
    }
}

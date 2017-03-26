using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Transform follow;

    Transform tran;
    private float y;

    // Use this for initialization
    void Start()
    {
        follow = FindObjectOfType<PlayerController>().transform;
        tran = transform;
        y = tran.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var x = iTween.FloatUpdate(tran.position.x, follow.position.x, 10);
        var z = iTween.FloatUpdate(tran.position.z, follow.position.z -15, 10);
        z = Mathf.Max(z, -20f);
        transform.position = new Vector3(x, y, z);
    }
}

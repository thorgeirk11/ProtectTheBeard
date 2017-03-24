using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    // Use this for initialization
    private float howLong;
	void Start () {
        howLong = 3f;
        StartCoroutine(DestroyMe());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(howLong);
        Destroy(this.gameObject);
    }
}

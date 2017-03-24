using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    // Use this for initialization
    [Range(3,10)]
    public float Duration;
	void Start () {
        StartCoroutine(DestroyMe());
	}

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var plane = new Plane(Vector3.up, -0.5f);

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            Vector3 dir = point - transform.position;
            dir.Normalize();
            GetComponent<Rigidbody>().velocity = dir * 20;
        }
        Destroy(this.gameObject, 2);
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Capsule")
        {
            Enemy enm = other.gameObject.transform.parent.GetComponent<Enemy>();
            enm.gotHit();
            SoundManager.Instance.PlaySpellImpactSound();
            Destroy(this.gameObject);
        }
        /*if(other.name == "Cube")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            Destroy(this.gameObject);
        }*/
    }
}

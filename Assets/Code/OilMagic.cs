using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OilMagic : MonoBehaviour
{
    // Use this for initialization
    public GameObject MagicEffect;
    public GameObject SwampEffect;
    private List<Enemy> enemiesThatEntered;
    private bool canDestroy = false;
    void Start()
    {
        enemiesThatEntered = new List<Enemy>();
        StartCoroutine(magicEffect());
        InvokeRepeating("DoDamage", 0, 1.0f);
    }
    private void DoDamage()
    {
        foreach (var item in enemiesThatEntered)
        {
            if(!item.Dead) item.gotHit();
        }
    }
    private IEnumerator magicEffect()
    {
        yield return new WaitForSeconds(1.5f);
        SwampEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        MagicEffect.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        SwampEffect.SetActive(false);
        canDestroy = true;
        RestoreThings();
    }
    private void RestoreThings()
    {
        foreach (var item in enemiesThatEntered)
        {
            if (item != null) item.restoreSpeed();
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!canDestroy)
        {
            if (other.name == "Capsule")
            {
                enemiesThatEntered.Add(other.gameObject.GetComponentInParent<Enemy>());
                other.gameObject.GetComponentInParent<NavMeshAgent>().speed = other.gameObject.GetComponentInParent<Enemy>().MoveSpeed / 2;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!canDestroy)
        {
            if (other.name == "Capsule")
            {
                other.gameObject.GetComponentInParent<NavMeshAgent>().speed = other.gameObject.GetComponentInParent<Enemy>().MoveSpeed / 2;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!canDestroy)
        {
            if (other.name == "Capsule")
            {
                enemiesThatEntered.Remove(other.gameObject.GetComponentInParent<Enemy>());
                other.gameObject.GetComponentInParent<NavMeshAgent>().speed *= 2;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Entity
{
    public float attackRate = 3f;
    public int HP { get; private set; }
    public bool ReachedEnterance { get; private set; }
    public float speed;
    public Slider healthBar;
    private Transform capsule;
    private PlayerController player;
    private float lastAttack;
    private NavMeshAgent agent;
    private int beardBalls;
    private Transform beard;
    private List<Transform> beardparts;
    private bool stopped { get; set; }
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        beardBalls = 0;
        stopped = false;
        beardparts = new List<Transform>();
        foreach (Transform item in transform)
        {
            if(item.name == "Capsule")
            {
                capsule = item;
            }
            if(item.name == "Beard")
            {
                beard = item;
            }
        }
        if(beard != null)
        {
            foreach (Transform item in beard)
            {
                item.gameObject.SetActive(false);
                beardparts.Add(item);
                
            }
        }
        speed = gameObject.GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped) gameObject.GetComponent<NavMeshAgent>().speed = 0;
        if (GameController.GameOver) return;
        var target = GameController.Instance.WizzardTransform;
        if (!ReachedEnterance)
        {
            target = getClosestCheckPoint();
        }
        agent.SetDestination(target.position);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (target.tag == "CheckPoint")
            {
                ReachedEnterance = true;
            }
            else if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                if (lastAttack < Time.time)
                {
                    lastAttack = Time.time + attackRate;
                    player.Damage();
                }
            }
        }

        healthBar.value = iTween.FloatUpdate(healthBar.value, HP, 2);
    }

    internal void Setup(EnemyData enemy)
    {
        HP = enemy.hp;
        healthBar.maxValue = HP;
        healthBar.value = HP;
    }
    private void AddBeard()
    {
        foreach (Transform item in beardparts)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
                break;
            }
        }
    }
    public void gotHit()
    {
        HP--;
        AddBeard();
        healthBar.value = HP;
        if (HP <= 0)
        {
            //Do something other than destroy it
            StartCoroutine(GoAway());
        }
    }
    private IEnumerator GoAway()
    {
        stopped = true;
        capsule.gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
    public void restoreSpeed()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
    }
    private Transform getClosestCheckPoint()
    {
        var minDistance = float.MaxValue;
        var target = transform;
        foreach (var i in GameController.Instance.FirstCheckPoints)
        {
            var tmpDist = Vector3.Distance(i.position, transform.position);
            if (minDistance > tmpDist)
            {
                target = i;
                minDistance = tmpDist;
            }
        }
        return target;
    }
}

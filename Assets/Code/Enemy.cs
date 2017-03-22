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

    public Slider healthBar;

    private PlayerController player;
    private float lastAttack;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameController.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
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

    internal void Setup(int hp)
    {
        HP = hp;
        healthBar.maxValue = HP;
        healthBar.value = HP;
    }

    public void gotHit()
    {
        HP--;
        healthBar.value = HP;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
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

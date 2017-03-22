using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Entity
{
    public Transform Target;
    public float attackRate = 3f;
    public int HP { get; private set; }
    public Slider healthBar;

    private PlayerController player;
    private bool canAttack = true;
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
        getClosestTarget();
        if (canAttack)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        StartCoroutine(AttackPlayer());
                    }
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

    private void getClosestTarget()
    {
        float minDistance = float.MaxValue;
        foreach (var item in GameController.Instance.getHiddens())
        {
            float tmpDist = Vector3.Distance(item.transform.position, this.gameObject.transform.position);
            if (minDistance > tmpDist)
            {
                Target = item.transform;
                minDistance = tmpDist;
            }

        }
        agent.SetDestination(Target.position);
    }
    private IEnumerator AttackPlayer()
    {
        canAttack = false;
        player.Damage();
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}

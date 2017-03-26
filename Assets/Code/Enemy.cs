using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Entity
{
    public int HP { get; private set; }
    public bool ReachedEnterance { get; private set; }
    public bool HasGottenAway { get; private set; }
    public bool Dead { get; private set; }
    public bool RunningAway { get; private set; }
    public int MaxHP { get; private set; }

    public float MoveSpeed { get; private set; }

    private Transform capsule;
    private NavMeshAgent agent;
    private Transform beard;
    private List<Transform> beardparts;
    private string path;
    private List<WizzardBeardSphere> wizzardStolenBeard;

    // Use this for initialization
    void Start()
    {
        wizzardStolenBeard = new List<WizzardBeardSphere>();
        agent = GetComponent<NavMeshAgent>();
        beardparts = new List<Transform>();
        foreach (Transform item in transform)
        {
            if (item.name == "Capsule")
            {
                capsule = item;
            }
            if (item.name == "Beard")
            {
                beard = item;
            }
        }
        if (beard != null)
        {
            foreach (Transform item in beard)
            {
                item.gameObject.SetActive(false);
                beardparts.Add(item);

            }
        }
        MoveSpeed = gameObject.GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.GameOver) return;
        if (HasGottenAway) return;

        var target = SelectTarget();
        agent.SetDestination(target.position);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (target.tag == "CheckPoint")
            {
                ReachedEnterance = true;
            }
            else if (target.tag == "Wizzard")
            {
                StealWizzardsBeard(target);
            }
            else if (target.tag == "WizzardBeard")
            {
                PickupWizzardsBeard(target.GetComponent<WizzardBeardSphere>());
            }
            else if (target.tag == "RunAwayPoint")
            {
                GotAway();
            }
        }

        //healthBar.value = iTween.FloatUpdate(healthBar.value, HP, 2);
    }

    private void GotAway()
    {
        HasGottenAway = true;
        gameObject.SetActive(false);
        //capsule.gameObject.GetComponent<Collider>().enabled = false;
    }


    private IEnumerable<Transform> OpenSphereSlots()
    {
        return from b in beardparts.Skip(MaxHP - HP).Take(HP)
               where b.GetComponentInChildren<WizzardBeardSphere>() == null
               select b;
    }

    public int HowManyBeardSpheresToSteel()
    {
        return OpenSphereSlots().Count();
    }

    private void StealWizzardsBeard(Transform target)
    {
        var steelAmount = HowManyBeardSpheresToSteel();
        var stolenBeard = BeardController.instance.StealWizzardsBeard(steelAmount);

        PlaceStolenBeard(stolenBeard);
    }

    private void PlaceStolenBeard(List<WizzardBeardSphere> stolenBeard)
    {
        var i = 0;
        var enemyBeard = OpenSphereSlots().ToList();
        foreach (var part in stolenBeard)
        {
            PlaceStolenBeard(part, enemyBeard[i]);
            i++;
        }
    }

    private void PlaceStolenBeard(WizzardBeardSphere stolen, Transform enemyPart)
    {
        enemyPart.GetComponent<Renderer>().enabled = false;
        enemyPart.gameObject.SetActive(true);
        stolen.transform.SetParent(enemyPart);
        stolen.EnemyPickingUp();
        wizzardStolenBeard.Add(stolen);
        iTween.MoveTo(stolen.gameObject, iTween.Hash("position", Vector3.zero, "islocal", true, "time", 1));
    }

    private void PickupWizzardsBeard(WizzardBeardSphere target)
    {
        if (HP >= 0 && HowManyBeardSpheresToSteel() > 0)
        {
            PlaceStolenBeard(new List<WizzardBeardSphere> { target });
        }
    }

    private Transform SelectTarget()
    {
        if (HP <= 0 || HowManyBeardSpheresToSteel() == 0)
        {
            RunningAway = true;
            return GameController.Instance.RunAwayTarget;
        }

        var target = GameController.Instance.WizzardTransform;
        var wizzardBeardParts = BeardController.instance.WizzardBeard
                                    .Where(i => i.HasBeenPickedUp && !i.IsOnEnemy);
        if (wizzardBeardParts.Any())
        {
            target = GetCloest(new[] { target }.Union(wizzardBeardParts.Select(i => i.transform)));
        }
        else if (!ReachedEnterance)
        {
            target = GetCloest(GameController.Instance.FirstCheckPoints);
        }
        return target;
    }

    internal void Setup(EnemyData enemy)
    {
        HP = enemy.hp;
        MaxHP = enemy.hp;
        path = "baddy";
        path += enemy.type.ToString();
        float scaling = enemy.type * 0.1f;
        gameObject.transform.localScale += new Vector3(scaling, scaling, scaling);
        Material mat = Resources.Load("BaddySprites/Materials/" + path) as Material;
        Transform ix = this.gameObject.transform.GetChild(0);
        ix.gameObject.GetComponent<Renderer>().material = mat;
    }
    private void AddBeard()
    {
        foreach (Transform part in beardparts)
        {
            var stolen = part.GetComponentInChildren<WizzardBeardSphere>();
            if (stolen != null)
            {
                wizzardStolenBeard.Remove(stolen);
                iTween.Stop(stolen.gameObject);
                stolen.EnemyDroped();
                stolen.transform.parent.GetComponent<Renderer>().enabled = true;
                stolen.transform.SetParent(null);
                return;
            }
            if (!part.gameObject.activeInHierarchy)
            {
                part.gameObject.SetActive(true);
                return;
            }
        }
    }
    public void gotHit()
    {
        HP--;
        AddBeard();

        //healthBar.value = HP;
        if (HP <= 0)
        {
            Material mat = Resources.Load("BaddySprites/Materials/" + path + "_happy") as Material;
            Transform ix = this.gameObject.transform.GetChild(0);
            ix.gameObject.GetComponent<Renderer>().material = mat;
            capsule.gameObject.GetComponent<Collider>().enabled = wizzardStolenBeard.Any();
            Dead = true;
        }
    }
    public void restoreSpeed()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = MoveSpeed;
    }
    private Transform GetCloest(IEnumerable<Transform> tranforms)
    {
        var minDistance = float.MaxValue;
        var target = transform;
        foreach (var i in tranforms)
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

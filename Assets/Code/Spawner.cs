using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool canSpawn = true;
    public int maxSpawns;
    private int currentSpawns = 0;
    public float spawnRate = 0.5f;
    private int highHP = 2;
    private Enemy enemyToSpawn;
    public List<EnemyData> enemyList;

    private void Awake()
    {
        makeNewList();
    }
    private void makeNewList()
    {
        enemyList = new List<EnemyData>();
    }
    void Update()
    {
        if (currentSpawns < maxSpawns)
        {
            if (canSpawn)
            {
                canSpawn = false;
                StartCoroutine(Spawn());
            }
        }
    }

    private IEnumerator Spawn()
    {
        SpawnObject();
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    }

    private void SpawnObject()
    {
        EnemyData enemyData = enemyList[0];
        switch (enemyData.type)
        {
            case 1:
                GameObject tmp = Resources.Load("Prefabs/Bearded Enemy") as GameObject;
                enemyToSpawn = tmp.GetComponent<Enemy>();
                break;
            default:
                break;
        }
        currentSpawns++;
        var enemy = Instantiate(enemyToSpawn);
        enemy.transform.position = this.transform.position;
        enemyList.RemoveAt(0);
        enemy.Setup(enemyData);
        var color = enemy.GetComponentInChildren<Renderer>().material.color;
        switch (enemy.HP)
        {
            case 2: color = Color.black; break;
            case 3: color = Color.red; break;
            case 4: color = Color.cyan; break;
            case 5: color = Color.magenta; break;
        }
        enemy.GetComponentInChildren<Renderer>().material.color = color;
    }
    public void resetSpawns()
    {
        currentSpawns = 0;
    }
    public void addEnemiesToWave(EnemyData enemy)
    {
        enemyList.Add(enemy);
    }
    public void startNewWave()
    {
        currentSpawns = 0;
        maxSpawns = enemyList.Count;
        spawnRate = Random.Range(0.5f, 2f);
    }
    public bool amDone()
    {
        return maxSpawns == currentSpawns;
    }
}

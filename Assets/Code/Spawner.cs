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
    public Enemy enemyToSpawn;

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
        currentSpawns++;
        var enemy = Instantiate(enemyToSpawn);
        enemy.transform.position = this.transform.position;
        enemy.Setup(Random.Range(1, Mathf.Min(highHP, 6)));
        var color = enemy.GetComponentInChildren<Renderer>().material.color;
        Debug.Log(highHP);
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
    public void startNewWave(int howmany, int highestHP)
    {
        currentSpawns = 0;
        maxSpawns = howmany;
        highHP = highestHP + 1;
        spawnRate = Random.Range(0.5f, 2f);

    }
}

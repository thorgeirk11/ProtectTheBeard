using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public static bool GameOver { get; internal set; }
    public bool timestopped { get; set; }
    public int MaxLifes;
    public IEnumerable<Transform> FirstCheckPoints { get; private set; }
    public Transform WizzardTransform { get; internal set; }
    public float OilBarFillRate;
    public Spawner[] spawners;

    private int waveNr;
    private WaveHandle allWaves;
    private int maxWaves;
    private LifeUI lifes;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        FirstCheckPoints = GameObject.FindGameObjectsWithTag("CheckPoint").Select(i => i.transform);
        WizzardTransform = GameObject.FindGameObjectWithTag("Wizzard").transform;
        //lifes = FindObjectOfType<LifeUI>();
        GameOver = false;
        var json = WaveHandle.LoadResourceTextfile("waveHandler.json");
        Debug.Log(json);
        allWaves = JsonUtility.FromJson<WaveHandle>(json);
        maxWaves = allWaves.waves.Length;
    }

    void Start()
    {
        StartCoroutine(checkForWaveDone());
        //lifes.SetMaxLifes(MaxLifes);
    }
    internal void PlayerGotHit()
    {
        var left = lifes.Lifes - 1;
        lifes.Lifes--;
        if (left == 0)
        {
            GameOver = true;
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

    private IEnumerator checkForWaveDone()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            bool check = true;
            foreach (var item in spawners)
            {
                check &= item.amDone();
                if (!check) break;
            }
            if (check)
            {
                while (FindObjectsOfType<Enemy>().Any())
                {
                    yield return new WaitForSeconds(1f);
                }
                spawnWave();
            }
        }
    }
    private void spawnWave()
    {
        List<EnemyData> enemies = splitUp(allWaves.waves[waveNr].enemies);
        int j = 0;
        foreach (var item in enemies)
        {
            spawners[j].addEnemiesToWave(item);
            j++;
            if (spawners.Length == j) j = 0;
        }
        foreach (var item in spawners)
        {
            item.startNewWave();
        }
        waveNr = waveNr >= maxWaves - 1 ? 0 : waveNr + 1;
    }
    private List<EnemyData> splitUp(EnemyData[] enemies)
    {
        List<EnemyData> arg = new List<EnemyData>();
        foreach (var item in enemies)
        {
            int x = item.amount;
            while (x > 0)
            {
                arg.Add(new EnemyData(item.type, item.hp, 1));
                x--;
            }
        }
        return arg;

    }
    public void stopTime()
    {
        if (timestopped)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        timestopped = !timestopped;
        PauseMenu.instance.show();
    }
}
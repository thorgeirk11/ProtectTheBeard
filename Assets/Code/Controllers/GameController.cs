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
    public Transform RunAwayTarget;

    //public List<Transform> StolenWizzardBeardParts;

    private int waveNr;
    private WaveHandle allWaves;
    private int maxWaves;
    //private LifeUI lifes;
    
    // Use this for initialization
    void Awake()
    {
        Instance = this;
        FirstCheckPoints = GameObject.FindGameObjectsWithTag("CheckPoint").Select(i => i.transform);
        WizzardTransform = GameObject.FindGameObjectWithTag("Wizzard").transform;
        GameOver = false;
        var json = WaveHandle.LoadResourceTextfile("waveHandler.json");
        Debug.Log(json);
        allWaves = JsonUtility.FromJson<WaveHandle>(json);
        maxWaves = allWaves.waves.Length;
    }

    void Start()
    {
        //SoundManager.Instance.PlayGameMusic();
        StartCoroutine(CheckForWaveDone());
    }


    private IEnumerator CheckForWaveDone()
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
                while (FindObjectsOfType<Enemy>().Any(x => !x.RunningAway))
                {
                    yield return new WaitForSeconds(1f);
                }
                SpawnWave();
            }
        }
    }
    private void SpawnWave()
    {
        var enemies = splitUp(allWaves.waves[waveNr].enemies);
        var j = 0;
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
        var arg = new List<EnemyData>();
        foreach (var item in enemies)
        {
            var x = item.amount;
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
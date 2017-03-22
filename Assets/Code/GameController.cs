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
    public bool timestopped { get; private set; }
    public PlayerController Player;
    public int MaxLifes;
    public IEnumerable<Transform> FirstCheckPoints { get; private set; }
    public Transform WizzardTransform { get; internal set; }

    private LifeUI lifes;
    public Spawner[] spawners;
    public GameObject pauseScreen;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        FirstCheckPoints = GameObject.FindGameObjectsWithTag("CheckPoint").Select(i => i.transform);
        WizzardTransform = GameObject.FindGameObjectWithTag("Wizzard").transform;
        lifes = FindObjectOfType<LifeUI>();
        GameOver = false;
    }

    void Start()
    {
        lifes.SetMaxLifes(MaxLifes);
        StartCoroutine(spawnWaves());
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
    private IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(2);
        for (int i = 1; ; i++)
        {
            foreach (Spawner spawner in spawners)
            {
                spawner.startNewWave(1 + i / 2, i / 3);
            }
            yield return new WaitForSeconds(10);
        }
    }

    public void stopTime()
    {
        if (timestopped)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        timestopped = !timestopped;
    }
    public void QuitGame()
    {
        Debug.Log("I SHOULD BE QUITTING RABBLE RABBLE");
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}

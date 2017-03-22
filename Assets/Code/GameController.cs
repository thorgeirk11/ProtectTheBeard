using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public static bool GameOver { get; internal set; }

    public PlayerController Player;
    public int MaxLifes;
    private GameObject[] hiddens;
    private LifeUI lifes;
    public GameObject[] spawners;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        lifes = GetComponentInChildren<LifeUI>();
        GameOver = false;
    }

    void Start()
    {
        hiddens = GameObject.FindGameObjectsWithTag("Hidden");
        lifes.SetMaxLifes(MaxLifes);
        StartCoroutine(spawnWaves());
    }
    public GameObject[] getHiddens()
    {
        return hiddens;
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
            foreach (GameObject item in spawners)
            {
                item.GetComponent<Spawner>().startNewWave(i,i/2);
            }
            yield return new WaitForSeconds(10);
        }
    }
}

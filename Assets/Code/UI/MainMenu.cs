﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.PlayMenuMusic();
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene(1);
    }
}

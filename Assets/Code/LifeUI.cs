using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{

    private int MaxLifes = 10;
    private Slider lifeBar;

    public int Lifes
    {
        get { return (int)lifeBar.value; }
        set { lifeBar.value = value; }
    }

    void Start()
    {
        lifeBar = GetComponent<Slider>();
    }

    internal void SetMaxLifes(int maxLifes)
    {
        lifeBar.maxValue = maxLifes;
        lifeBar.minValue = 0;
    }
}

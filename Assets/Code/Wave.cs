using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave {
    public int wavenr;
    public EnemyData[] enemies;


    public override string ToString()
    {
        string ret = "WaveNr: " + wavenr;
        foreach (var item in enemies)
        {
            ret +=", "+ item.ToString();
        }

        return ret;
    }
}

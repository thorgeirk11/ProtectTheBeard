using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class WaveHandle
{
    public Wave[] waves;
    public static string LoadResourceTextfile(string path)
    {

        string filePath = "Text/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }

    public override string ToString()
    {
        string ret = "Waves: ";
        foreach (var item in waves)
        {
            ret += item.ToString() + ",";
        }
        return ret;
    }

}

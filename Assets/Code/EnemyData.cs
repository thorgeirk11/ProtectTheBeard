using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyData {
    public int type;
    public int hp;
    public int amount;
    public EnemyData(int type, int hp, int amount)
    {
        this.type = type;
        this.hp = hp;
        this.amount = amount;
    }

    public override string ToString()
    {
        string ret = "EnemyType : " + type + ", HP: " + hp + ", Amount: " + amount +". ";
        return ret;
    }
}

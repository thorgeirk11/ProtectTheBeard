using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected void Wait(float sec, Action action)
    {
        StartCoroutine(Wait(new WaitForSeconds(sec), action));
    }

    private IEnumerator Wait(YieldInstruction wait, Action action)
    {
        yield return wait;
        action();
    }
}

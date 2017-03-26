using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmitter : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Clips;
    [SerializeField]
    bool DoNotDestroy;
    public bool loop;

    public void Start()
    {
        if (Clips.Length == 0)
        {
            print(name + " is missing a audio clip.");
            return;
        }

        if (DoNotDestroy) DontDestroyOnLoad(gameObject);

        var index = Random.Range(0, Clips.Length);
        var source = GetComponent<AudioSource>();
        source.clip = Clips[index];
        source.Play();
        source.loop = loop;
        if(!loop)Destroy(gameObject, Clips[index].length);
    }
}

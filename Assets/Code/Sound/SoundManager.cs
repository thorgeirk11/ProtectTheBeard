using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    private SoundManager() { }

    public void Awake()
    {
        if (Instance != null)
        {
            print("Two Soundmanagers! not good!");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void MuteSounds()
    {
        Instance.MainMixer.SetFloat("MasterVolume", -80f);
    }
    public void UnmuteSounds()
    {
        Instance.MainMixer.SetFloat("MasterVolume", 0);
    }

    public AudioMixer MainMixer;
    public AudioEmitter PlayButtonSound;
    public AudioEmitter CollisionSound;
    public AudioEmitter BeardInAirSound;
    public AudioEmitter MenuMusic;
    public AudioEmitter GameMusic;

    public AudioEmitter BeardReturnSound;

    public AudioEmitter HealSpellSound;
    public AudioEmitter LoseScreenSound;
    public AudioEmitter MenubuttonClickSound;
    public AudioEmitter OilSpellSound;
    public AudioEmitter PewPewSound;
    public AudioEmitter PickUpSound;
    public AudioEmitter SpellImpactSound;
    public AudioEmitter WallSpellSound;

    internal void PlayBeardReturnSoundc()
    {
        Instantiate(BeardReturnSound);
    }
    internal void PlayHealSpellSound()
    {
        Instantiate(HealSpellSound);
    }
    internal void PlayLoseScreenSound()
    {
        Instantiate(LoseScreenSound);
    }
    internal void PlayMenubuttonClickSound()
    {
        Instantiate(MenubuttonClickSound);
    }
    internal void PlayOilSpellSound()
    {
        Instantiate(OilSpellSound);
    }
    internal void PlayPewPewSound()
    {
        Instantiate(PewPewSound);
    }
    internal void PlayPickUpSound()
    {
        Instantiate(PickUpSound);
    }
    internal void PlayWallSpellSound()
    {
        Instantiate(WallSpellSound);
    }
    internal void PlaySpellImpactSound()
    {
        Instantiate(SpellImpactSound);
    }

    internal void PlayMenuMusic()
    {
        Instantiate(MenuMusic);
    }
    internal void PlayGameMusic()
    {
        Instantiate(GameMusic);
    }

    public void PlayMenuButtonClickSound()
    {
        Instantiate(MenubuttonClickSound);
    }
    public void PlayButtonsSound()
    {
        Instantiate(PlayButtonSound);
    }
    public void PlayBeardInAirSound()
    {
        Instantiate(BeardInAirSound);
    }

    internal void PlayCollisionSound()
    {
        Instantiate(CollisionSound);
    }


}

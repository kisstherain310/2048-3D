using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Music = 0,
    JokerExplore = 1,
    BombExplore = 2,
    JokerShot = 3,
    BombShot = 4,
    BoxShot = 5,
    ButtonClick = 6,
    SmallSuccess = 7,
    // Combo
    comboHit1 = 8,
    comboHit2 = 9,
    comboHit3 = 10,
    comboHit4 = 11,
    comboHit5 = 12,
    comboHit6 = 13,
    comboHit7 = 14,
    comboHit8 = 15,
    comboHit9 = 16,
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    void Awake()
    {
        instance = this;
    }
    [SerializeField] private AudioSource[] audios;
    public bool isVibrate = true;
    public bool isSound = true;
    public bool isMusic = true;
    public void Initialize()
    {
        if(!isMusic) audios[0].Stop();
        for (int i = 1; i < audios.Length; i++) audios[i].Stop();
    }
    public void SetState(bool isVibrate, bool isSound, bool isMusic)
    {
        this.isVibrate = isVibrate;
        this.isSound = isSound;
        this.isMusic = isMusic;
    }
    public void PlayClip(AudioType type)
    {
        if ((int)type > 0 && !isSound) return;
        audios[(int)type].Play();
    }
    public void StopClip(AudioType type)
    {
        audios[(int)type].Stop();
    }
    public void PlayComboHit(int number)
    {
        if (!isSound) return;
        int num = ((int)Math.Log(number, 2) - 2) % 8;
        audios[num + 8].Play();
    }
}

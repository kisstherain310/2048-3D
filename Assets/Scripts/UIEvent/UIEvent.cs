using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEvent : MonoBehaviour
{
    [SerializeField] public EventButton eventButton;
    [SerializeField] public EventJokerCube eventJokerCube;
    [SerializeField] public EventBombCube eventBombCube;
    [SerializeField] private GameObject[] checkbox;
    [SerializeField] private GameObject[] tick;
    private bool[] stateTick;
    void Start()
    {
        stateTick = new bool[checkbox.Length];
        for (int i = 0; i < stateTick.Length; i++)
        {
            stateTick[i] = true; 
            int index = i; 
            checkbox[i].GetComponent<Button>().onClick.AddListener(() => ToggleTick(index));
            tick[i].GetComponent<Button>().onClick.AddListener(() => ToggleTick(index));
        }
    }
    public void OnPlay()
    {
        GameManager.Instance.OnPlay();
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
        SoundManager.instance.PlayClip(AudioType.SmallSuccess);
    }
    
    public void OnRestart()
    {
        OnPlay();
        eventButton.HideSetting();
        FXManager.Instance.PlayFX(Vector3.zero, FXType.ReplayEffect);
    }
    private void ToggleTick(int index)
    {
        stateTick[index] = !stateTick[index];
        tick[index].SetActive(stateTick[index]);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
        SoundManager.instance.isVibrate = stateTick[0];
        SoundManager.instance.isSound = stateTick[1];
        SoundManager.instance.isMusic = stateTick[2];
        GameManager.Instance.dataManager.SaveGameState();
        if(index == 2) PlayMusic();
    }
    public void RestoreSetting(){
        stateTick[0] = SoundManager.instance.isVibrate;
        stateTick[1] = SoundManager.instance.isSound;
        stateTick[2] = SoundManager.instance.isMusic;
        for (int i = 0; i < stateTick.Length; i++) tick[i].SetActive(stateTick[i]);
    }
    private void PlayMusic(){
        if(stateTick[2]) SoundManager.instance.PlayClip(AudioType.Music);
        else SoundManager.instance.StopClip(AudioType.Music);
    }
}

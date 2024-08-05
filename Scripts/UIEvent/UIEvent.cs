using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEvent : MonoBehaviour
{
    [SerializeField] private GameObject buttonSetting;
    [SerializeField] private GameObject skins;
    [SerializeField] private GameObject[] checkbox;
    [SerializeField] private GameObject[] tick;
    [SerializeField] private ScrollToTop scroll;
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
    public void ShowSetting()
    {
        buttonSetting.SetActive(true); 
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void HideSetting()
    {
        buttonSetting.SetActive(false);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void OnRestart()
    {
        OnPlay();
        HideSetting();
        FXManager.Instance.PlayFX(Vector3.zero, FXType.ReplayEffect);
    }
    private void ToggleTick(int index)
    {
        stateTick[index] = !stateTick[index];
        tick[index].SetActive(stateTick[index]);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
        UpdateGame();
    }
    private void UpdateGame(){
        if(stateTick[2]) SoundManager.instance.PlayClip(AudioType.Music);
        else SoundManager.instance.StopClip(AudioType.Music);

        if(stateTick[1]) SoundManager.instance.isSound = true;
        else SoundManager.instance.isSound = false;

        if(stateTick[0]) SoundManager.instance.isVibrate = true;
        else SoundManager.instance.isVibrate = false;
    }
    public void OpenShop(){
        skins.SetActive(true);
        scroll.FixOnTop();
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void OpenHome(){
        skins.SetActive(false);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
}

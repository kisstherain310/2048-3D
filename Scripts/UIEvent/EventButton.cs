using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonSetting;
    [SerializeField] private GameObject skins;
    [SerializeField] private GameObject ads;
    [SerializeField] private GameObject notiCube;
    [SerializeField] private TMP_Text numberCube;
    [SerializeField] private ScrollToTop scroll;
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
    public void OpenShop(){
        skins.SetActive(true);
        scroll.FixOnTop();
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void CloseShop(){
        skins.SetActive(false);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void OpenAds(){
        ads.SetActive(true);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void CloseAds(){
        ads.SetActive(false);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
    }
    public void OpenNotiCube(){
        if(notiCube.activeSelf) return;
        notiCube.SetActive(true);
        if(Utilities.Random66()) {
            notiCube.transform.GetChild(1).GetChild(1).gameObject.SetActive(true); 
            notiCube.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            GameManager.Instance.uIEvent.eventJokerCube.IncreaseCount();
        }
        else {
            notiCube.transform.GetChild(1).GetChild(1).gameObject.SetActive(false); 
            notiCube.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
            GameManager.Instance.uIEvent.eventBombCube.IncreaseCount();
        }
        notiCube.transform.GetChild(1).GetComponent<InitEffect>().growEffect();
        numberCube.text = Utilities.ProcessNumber(GameManager.Instance.classicCubeManager.maxCube);
    }
    public void CloseNotiCube(){
        notiCube.SetActive(false);
        SoundManager.instance.PlayClip(AudioType.ButtonClick);
        GameManager.Instance.uIEvent.eventBombCube.SetText();
        GameManager.Instance.uIEvent.eventJokerCube.SetText();
    }
}

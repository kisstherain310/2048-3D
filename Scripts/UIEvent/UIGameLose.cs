using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameLose : MonoBehaviour
{
    [SerializeField] private CountDownTimer countDownTimer;
    public void Show()
    {
        gameObject.SetActive(true);
        OnActive();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnActive()
    {
        countDownTimer.CountDown();
    }
}

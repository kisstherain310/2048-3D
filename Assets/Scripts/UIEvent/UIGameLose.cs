using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameLose : MonoBehaviour
{
    [SerializeField] private CountDownTimer countDownTimer;
    public void Show()
    {
        gameObject.SetActive(true);
        countDownTimer.CountDown();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

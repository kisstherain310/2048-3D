using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    private float countdownTime = 10f; // Thời gian đếm ngược
    [SerializeField] private Image circleImage; // Hình ảnh hình tròn với shader RadialFill
    [SerializeField] private TMP_Text seconds; // Hiển thị số giây còn lại

    private float elapsedTime = 0f;

    public void CountDown()
    {   
        StartCoroutine(ICountDown());
    }
    IEnumerator ICountDown(){
        circleImage.fillAmount = 1.0f;
        elapsedTime = 0f;
        while (elapsedTime < countdownTime)
        {
            elapsedTime += Time.deltaTime;
            seconds.text = (countdownTime - elapsedTime).ToString("0");
            circleImage.fillAmount = 1.0f - (elapsedTime / countdownTime);
            yield return null;
        }
        circleImage.fillAmount = 0f;
    }
}

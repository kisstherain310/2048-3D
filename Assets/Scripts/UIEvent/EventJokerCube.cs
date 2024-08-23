using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventJokerCube : MonoBehaviour
{
    [SerializeField] private TMP_Text count;
    public int countJokerCube = 10;
    public void OnMouseDown()
    {
        if(GameManager.Instance.gameStatus.IsPlaying() == false) return;
        if(countJokerCube <= 0) return;
        VibrationManagerX.Vibrate();
        GameManager.Instance.jokerCubeManager.SpawnJokerCube();
        GameManager.Instance.moveManager.isActive = true;
    }
    public void SetCount(int number){
        countJokerCube = number;
        count.text = number.ToString();
    }
    public void SetText() => count.text = countJokerCube.ToString();
    public void DecreaseCount() => countJokerCube--;
    public void IncreaseCount() => countJokerCube++;
    
}

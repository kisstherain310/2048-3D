using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventBombCube : MonoBehaviour
{
    [SerializeField] private TMP_Text count;
    public int countBombCube = 10;
    public void OnMouseDown()
    {
        if(countBombCube <= 0) return;
        VibrationManagerX.Vibrate();
        GameManager.Instance.bombCubeManager.SpawnBombCube();
        GameManager.Instance.moveManager.isActive = true;
    }
    public void SetCount(int number){
        countBombCube = number;
        count.text = number.ToString();
    }
    public void DecreaseCount(){
        countBombCube--;
        count.text = countBombCube.ToString();
    }
    public void IncreaseCount(){
        countBombCube++;
        count.text = countBombCube.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private bool isGameOver = false;
    public bool IsLose() {
        return !isGameOver;
    }
    public void OnGameOver() {
        isGameOver = true;
    }
    public void OnPlay(){
        isGameOver = false;
    }
}

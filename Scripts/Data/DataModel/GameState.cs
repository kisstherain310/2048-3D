using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public List<InforClassicCube> listCubes;
    public List<InforSpecialCube> listSpecialCubes;
    public InforClassicCube nextCube;
    public int score;
    public int highScore;
    public int countBombCube;
    public int countJokerCube;
    public bool isVibrate;
    public bool isSound;
    public bool isMusic;
    public int indexMainBg;
    public int maxCube;
}

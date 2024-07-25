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
}

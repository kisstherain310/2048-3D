using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JokerCube : BaseCube
{
    [SerializeField] private JokerMove cubeMove;
    protected override void SetPoolTag()
    {
        poolTag = "JokerCube";
    }
    protected override void InitCubeMove()
    {
        cubeMove.GetCube(this);
    }
    protected override void DecreaseCube()
    {
        GameManager.Instance.uIEvent.eventJokerCube.DecreaseCount();
        GameManager.Instance.uIEvent.eventJokerCube.SetText();

    }
}

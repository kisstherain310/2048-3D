using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombCube : BaseCube
{
    [SerializeField] private BombMove cubeMove;
    protected override void SetPoolTag()
    {
        poolTag = "BombCube";
    }
    protected override void InitCubeMove()
    {
        cubeMove.GetCube(this);
    }
    protected override void DecreaseCube()
    {
        GameManager.Instance.uIEvent.eventBombCube.DecreaseCount();
        GameManager.Instance.uIEvent.eventBombCube.SetText();
    }
}

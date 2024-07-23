using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

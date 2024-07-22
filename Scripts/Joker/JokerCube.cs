using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerCube : BaseCube
{
    [SerializeField] private JokerMove cubeMove;

    protected override void InitCubeMove()
    {
        cubeMove.GetCube(this);
    }
}

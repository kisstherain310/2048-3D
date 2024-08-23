using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : BaseCube
{
    [SerializeField] public CubeUI cubeUI;
    [SerializeField] public CubeMove cubeMove;
    [SerializeField] public CubeX2Move cubeX2Move;
    [SerializeField] public SpawnEffect spawnEffect;
    [HideInInspector] public int cubeNumber;
    protected override void SetPoolTag()
    {
        poolTag = "ClassicCube";
    }
    protected override void InitCubeMove()
    {
        cubeMove.GetCube(this);
    }
    // ---- Helper Method --------------------------------
    public float getMaxPosx()
    {
        return cubeMove.maxPosx;
    }
    public void EditCube(int number)
    {
        cubeNumber = number;
        cubeUI.EditCube(number);
    }
}

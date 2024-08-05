using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : BaseMove
{
    private Cube cube;
    public void GetCube(Cube cube)
    {
        this.cube = cube;
    }
    protected override bool IsMainCube()
    {
        return cube.isMainCube;
    }
    protected override void HandlePointerUp()
    {
        cube.handlePointerUp(); 
    }
}